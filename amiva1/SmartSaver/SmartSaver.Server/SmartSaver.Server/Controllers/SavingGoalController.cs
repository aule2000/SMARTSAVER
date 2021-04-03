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
    [Route("savings")]
    [ApiController]
    public class SavingGoalController : ControllerBase
    {
        private readonly ISavingGoalsRepository _savingGoalsRepository;

        public SavingGoalController(ISavingGoalsRepository savingGoalsRepository)
        {
            _savingGoalsRepository = savingGoalsRepository;
        }
    
        [HttpGet("{id}/sorting")]
        public async Task<IReadOnlyList<SavingGoal>> Get(Guid userId, string sortingColumn, bool isAscending)
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
            return await _savingGoalsRepository.GetSortedGoals(user.Id,
                new SortingModel { IsAscending = isAscending, SortingColumn = sortingColumn });
        }

        [HttpPost]
        public async Task<Guid> Store(SavingGoal goal)
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
            goal.UserId = user.Id;
            return await _savingGoalsRepository.Create(goal);
        }

        [Route("goaledit/{id}")]
        [HttpDelete]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _savingGoalsRepository.Delete(id);
            return NoContent();
        }

        [Route("savings/goaledit")]
        [HttpPut]
        public async void Update(SavingGoal goal)
        {
            await _savingGoalsRepository.Update(goal.Id, goal);
        }
       


    }
}
