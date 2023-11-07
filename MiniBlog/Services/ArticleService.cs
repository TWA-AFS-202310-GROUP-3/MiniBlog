using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService
{
    private readonly ArticleStore articleStore = null!;
    private readonly UserStore userStore = null!;
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(ArticleStore articleStore, UserStore userStore, IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleStore = articleStore;
        this.userStore = userStore;
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    /*public async Task<Article?> CreateArticle(Article article)
    {
        if (article.UserName != null)
        {
            if (await userRepository.GetByName(article.UserName) == null)
            {
                await userRepository.Create(new User(article.UserName));
            }
        }

        return await articleRepository.CreateArticle(article);
    }*/
    public async Task<Article?> CreateArticle(Article article)
    {
        if (article.UserName != null)
        {
            var result = userRepository.GetByName(article.UserName);
            if (result == null)
            {
                _ = userRepository.Create(new User(article.UserName));
            }
        }

        return await articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article> GetById(string id)
    {
        return await articleRepository.GetById(id);
        //return articleStore.Articles.FirstOrDefault(article => article.Id == id.ToString());
    }
}
