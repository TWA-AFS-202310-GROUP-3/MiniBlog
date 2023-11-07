using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IArticleRepository articleRepository = null!;
        private readonly IUserRepository userRepository = null!;
        public UserService(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            return await userRepository.CreateUser(user);
        }
    }
}
