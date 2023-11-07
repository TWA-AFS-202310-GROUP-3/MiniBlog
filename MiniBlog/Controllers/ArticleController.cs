using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly ArticleService articleService = null!;
        private readonly UserService userService = null!;

        public ArticleController(ArticleStore articleStore, UserStore userStore, UserService userService, ArticleService articleService)
        {
            //this.articleStore = articleStore;
            //this.userStore = userStore;
            this.userService = userService;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<List<Article>> ListAsync()
        {
            Console.WriteLine(articleService.GetAllAsync());
            return await articleService.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Article article)
        {
            var createdArticle = await articleService.CreateArticleAsync(article);

            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public async Task<Article> GetById(string id)
        {
            return await articleService.GetByIdAsync(id);
        }
    }
}
