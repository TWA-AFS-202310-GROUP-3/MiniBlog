using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniBlog.Services
{
    public class UserService
    {
        private ArticleStore articleStore = null;
        private UserStore userStore = null;
        private IUserRepository userRepository = null;

        public UserService(ArticleStore articleStore, UserStore userStore, IUserRepository userRepository)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            User createdUser = null;
            if (newUser != null)
            {
                await userRepository.CreateUser(newUser);
            }

            createdUser = await userRepository.GetUser(newUser.Name);

            return createdUser;
        }

        public User Delete(string name)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
            }

            return foundUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User> GetUser(string name)
        {
            return await userRepository.GetUser(name);
        }

        public User Update(User user)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }
    }
}
