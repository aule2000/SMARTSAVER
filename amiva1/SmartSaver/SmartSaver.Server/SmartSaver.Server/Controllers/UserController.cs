using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSaver.Domain;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;

namespace SmartSaver.Server.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //[HttpGet]
        //[Route("getid")]
        //public async Task<User> Get(Guid userId)
        //{
        //    return await _usersRepository.GetById(userId);
        //}

        [HttpGet]
        [Route("parser")]                           //all usershttps://localhost:5001/users/parser
        public  async Task<IReadOnlyList<User>> ParseUser()
        {
            return await _usersRepository.GetAll();
        }


        [HttpPut]
        [Route("update")]
        public void Put(User user)
        {
           _usersRepository.Update(user.Id, user);
        }

        [HttpPost]
        [Route("add")]
        public async Task<Guid> CreateAsync([FromBody] User _user)
        {
               return await _usersRepository.Create(_user);
        }

    }
}
