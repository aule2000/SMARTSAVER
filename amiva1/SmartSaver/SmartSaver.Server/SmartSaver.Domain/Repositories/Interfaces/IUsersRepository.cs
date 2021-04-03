using SmartSaver.Domain.Models;
using System.Threading.Tasks;
namespace SmartSaver.Domain.Repositories.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        public Task<User> GetByEmail(string username);

        public Task<User> AuthenticateAsync(string email, string password);
    }
}
