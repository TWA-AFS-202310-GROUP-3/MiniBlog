using MiniBlog.Model;
using MiniBlog.Repositories;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository = null!;

        public UserService(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetByName(string username) 
            => await userRepository.GetByName(username);
    }
}