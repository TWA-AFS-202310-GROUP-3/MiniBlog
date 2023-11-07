using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly ArticleService articleService = null!;
        private readonly UserService userService = null!;

        public UserController(ArticleStore articleStore, UserService userService, UserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
           await userService.CreateUserAsync(user);
           return CreatedAtAction(nameof(GetByNameAsync), new { name = user.Name }, GetByNameAsync(user.Name));
        }

        [HttpGet]
        public async Task<List<User>> GetAllAsync()
        {
            return await userService.GetAllAsync();
        }

        [HttpPut]
        public User Update(User user)
        {
            return userService.Update(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            return userService.Delete(name);
        }

        [HttpGet("{name}")]
        public async Task<User> GetByNameAsync(string name)
        {
            return await userService.GetUser(name);
        }
    }
}
