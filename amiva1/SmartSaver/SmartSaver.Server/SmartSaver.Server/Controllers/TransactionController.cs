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
    [Route("transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public TransactionController(ITransactionsRepository transactionsRepository, IUsersRepository usersRepository, ICategoriesRepository categoriesRepository)
        {
            _transactionsRepository = transactionsRepository;
            _usersRepository = usersRepository;
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet("{id}/sorting")]
        public async Task<IReadOnlyList<Transaction>> Get(Guid userId, string sortingColumn, bool isAscending)
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
            return await _transactionsRepository.GetSortedUserTransactions(user.Id,
                new SortingModel { IsAscending = isAscending, SortingColumn = sortingColumn });
        }

        [HttpGet("last/{count}")]
        public async Task<IReadOnlyList<Transaction>> GetLast(int count)
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
            return await _transactionsRepository.GetLastTransactions(user.Id, count);
        }

        [HttpGet("grouped")]
        public async Task<IReadOnlyList<GroupedTransaction>> GetGroupedByCategory()
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
            return await _transactionsRepository.GetAmountSpentPerCategory(user.Id);
        }


        [HttpPost]
        [Route("spend")]
        public async Task<IActionResult> AddNewSpending(Transaction transaction)
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
            var user1 = await _usersRepository.GetById(user.Id);
            var balance = user1.GetType().GetProperty(transaction.BalanceType);
            var balanceAmount = (double)balance.GetValue(user1);

            transaction.UserId = user1.Id;

            if (balanceAmount >= transaction.AmountDouble)
            {
                balance.SetValue(user1, balanceAmount - transaction.AmountDouble);

                await _usersRepository.Update(user1.Id, user1);
                await _transactionsRepository.Create(transaction);

                return Ok();
            }

            return NotFound();
        }

        [HttpPut("{userId}/{type}/{category}/{description}/{amount}")]
        public async Task<ActionResult> SpendExtension(Guid userId, string type, string category, string description, string amount)
        {
            var getCategory = await _categoriesRepository.GetCategoryByName(category);

            if (getCategory != null)
            {
                var user = await _usersRepository.GetById(Domain.Constants.Constants.TestUserId);
                var balance = user.GetType().GetProperty(type);
                var balanceAmount = (double)balance.GetValue(user);

                if (double.TryParse(amount, out var spendAmount))
                {
                    if (balanceAmount >= spendAmount)
                    {
                        var transaction = new Transaction
                        {
                            AmountDouble = spendAmount,
                            BalanceType = type,
                            CategoryId = getCategory.Id,
                            CreatedAt = DateTime.UtcNow,
                            Description = description,
                            UserId = userId
                        };

                        balance.SetValue(user, balanceAmount - spendAmount);

                        await _transactionsRepository.Create(transaction);
                        await _usersRepository.Update(user.Id, user);

                        return Ok();
                    }
                }
            }

            return NotFound();
        }
    }
}
