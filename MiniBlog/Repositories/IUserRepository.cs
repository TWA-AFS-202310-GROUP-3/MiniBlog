using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<User> AddUser(User user);
        public Task<User> GetUserByName(string userName);
    }
}
