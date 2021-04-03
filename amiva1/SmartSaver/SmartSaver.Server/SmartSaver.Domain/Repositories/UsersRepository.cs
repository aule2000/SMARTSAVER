using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(SmartSaverContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var users = await GetAll();
            foreach (User u in users)
            {
                if (u.Email == email)
                    return u;
            }

            return null;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            if (email == null || password == null)
                return null;

            var users = await GetAll();

            var hashedPassword = UserUtilities.EncryptPassword(password, email);
            foreach (User u in users)
            {
                if (u.Email == email && u.HashedPassword == hashedPassword)
                    return u;
            }
            return null;
        }
    }
}
