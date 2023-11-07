using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly ArticleStore articleStore = null;
        private readonly UserStore userStore = null;
        private readonly IArticleRepository articleRepository = null;
        private readonly IUserRepository userRepository = null;

        public ArticleService(ArticleStore articleStore, UserStore userStore, IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            Article createdArticle = null;
            if (article.UserName != null)
            {
                if (await userRepository.GetUser(article.UserName) == null)
                {
                    await userRepository.CreateUser(new User(article.UserName));
                }

                createdArticle = await articleRepository.CreateArticle(article);
            }

            return await GetByIdAsync(createdArticle?.Id);
        }

        public async Task<Article> GetByIdAsync(string id)
        {
            return await articleRepository.GetArticle(id);
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await articleRepository.GetAllArticles();
        }
    }
}
