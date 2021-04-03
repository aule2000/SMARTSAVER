using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSaver.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartSaver.Server.Controllers
{
    /// <summary>
    /// Get current fuel prices at various gas stations
    /// </summary>
    [ApiExplorerSettings(GroupName = "client")]
    [Authorize]
    [Produces("application/json")]
    [Route("pricer")]
    [ApiController]
    public class PricerController : ControllerBase
    {
        [HttpGet]
        public Dictionary<string, List<Fuel>> GetFuelPrices()
        {
            var priceList = new Dictionary<string, List<Fuel>>
            {
                { "gassoline", ScrapeFuel("https://pricer.lt/tyrimai/preke/pigiausias-a95-oktaninio-skaiciaus-benzinas/53") },
                { "diesel", ScrapeFuel("https://pricer.lt/tyrimai/preke/pigiausias-dyzelinas/58") },
                { "gas", ScrapeFuel("https://pricer.lt/tyrimai/preke/suskystintos-automobilines-dujos/60") },
                { "dieselplus", ScrapeFuel("https://pricer.lt/tyrimai/preke/dyzelinas-su-multifunkciniais-priedais/59") },
                { "gassolineplus", ScrapeFuel("https://pricer.lt/tyrimai/preke/a95-oktaninio-skaiciaus-benzinas-su-multifunkciniais-priedais/54") }
            };

            return priceList;
        }

        private static List<Fuel> ScrapeFuel(string url)
        {
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(url);
            doc.DocumentNode.SelectSingleNode("//table[@class='uk-table uk-table-divider uk-table-striped uk-table-hover']");
            var prices = doc.DocumentNode.SelectNodes("//span[@class='product-price uk-label uk-label-danger']");

            var test = doc.DocumentNode.InnerHtml.Replace("\n", "").Replace(" ", "");

            var pattern = "(?<=style=\"max-width:100px\"></td><td>)(.*?)(?=</td>)";
            var names = Regex.Matches(test, pattern);

            return prices.Select((t, i) => new Fuel { Company = names[i].Value, Price = t.InnerText }).ToList();
        }


    }
}
