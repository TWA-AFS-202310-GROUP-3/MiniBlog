using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly UserRepository userRepository = null!;

        public UserService(ArticleStore articleStore, UserStore userStore, UserRepository userRepository)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.userRepository = userRepository;
        }
    }
}
