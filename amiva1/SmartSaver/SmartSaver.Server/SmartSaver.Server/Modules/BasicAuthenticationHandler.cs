using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SmartSaver.Server
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsersRepository _db;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUsersRepository db)
            : base(options, logger, encoder, clock)
        {
            _db = db;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.Run(async () =>
            {
                // skip authentication if endpoint has [AllowAnonymous] attribute
                var endpoint = Context.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                    return AuthenticateResult.NoResult();

                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    Context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"AMIVA Smart Saver API\"");
                    return AuthenticateResult.Fail("Missing Authorization Header");
                }

                User user = null;
                try
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                    var email = credentials[0];
                    var password = credentials[1];

                    user = await _db.AuthenticateAsync(email, password);
                }
                catch
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }

                if (user == null)
                    return AuthenticateResult.Fail("Invalid Username or Password");

                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Trim()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            });
        }
    }
}