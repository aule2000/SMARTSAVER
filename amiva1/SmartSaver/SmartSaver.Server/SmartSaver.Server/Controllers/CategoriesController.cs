using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;

namespace SmartSaver.Server.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Category>> Get([FromQuery(Name = "per-page")] int perPage = 10, [FromQuery(Name = "page")] int page = 1)
        {
            User user = new User();
            HttpClient client = new HttpClient();

            var req = await client.GetAsync("https://localhost:5001/users/parser");
            Task<string> c = req.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<List<User>>(c.Result);
            foreach (var g in res)
            {
                //Console.WriteLine("sadsasda " + g.Gmail);
                if (g.Logged == true)
                {
                   // Console.WriteLine("sadasdas");
                    user = new User()
                    {
                        Id = g.Id,    //randomize
                        Gmail = g.Gmail,
                        Card = g.Card,
                        Cash = g.Cash,
                        FullName = g.FullName,
                        UserImage = g.UserImage,
                        Logged = true,
                        Password = g.Password
                    };
                }
            }
            return await _categoriesRepository.GetAllUserCategories(user.Id, perPage, page);
        }

        [HttpPost]
        public async Task<Guid> Store(Category category)
        {
           

            User user = new User();
            HttpClient client = new HttpClient();

            var req = await client.GetAsync("https://localhost:5001/users/parser");
            Task<string> c = req.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<List<User>>(c.Result);
            foreach (var g in res)
            {
                if (g.Logged == true)
                {
                    user = new User()
                    {
                        Id = g.Id,   
                        Gmail = g.Gmail,
                        Card = g.Card,
                        Cash = g.Cash,
                        FullName = g.FullName,
                        UserImage = g.UserImage,
                        Logged = true,
                        Password = g.Password
                    };
                }
            }
            category.UserId = user.Id;
            return await _categoriesRepository.Create(category);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _categoriesRepository.Delete(id);

            return NoContent();
        }

        [Route("count")]
        [HttpGet]
        public async Task<ActionResult<int>> GetCount()
        {
            User user = new User();
            HttpClient client = new HttpClient();

            var req = await client.GetAsync("https://localhost:5001/users/parser");
            Task<string> c = req.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<List<User>>(c.Result);
            foreach (var g in res)
            {
                if (g.Logged == true)
                {
                    user = new User()
                    {
                        Id = g.Id,
                        Gmail = g.Gmail,
                        Card = g.Card,
                        Cash = g.Cash,
                        FullName = g.FullName,
                        UserImage = g.UserImage,
                        Logged = true,
                        Password = g.Password
                    };
                }
            }
            return await _categoriesRepository.GetCount(user.Id);
        }
    }
}
