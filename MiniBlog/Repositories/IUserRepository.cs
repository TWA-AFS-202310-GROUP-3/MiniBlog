using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> CreateUser(User user);
        public Task<User> GetUser(string name);
        public Task<User> Update(User user);
        public Task<User> Delete(string name);
    }
}
