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
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        if (article.UserName != null)
        {
            if (await userRepository.GetUserByName(article.UserName) != null)
            {
                await userRepository.CreateUser(new User { Name = article.UserName });
            }

            await articleRepository.CreateArticle(article);
        }

        return article;
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetAll();
    }

    public async Task<Article?> GetById(Guid id)
    {
        return await articleRepository.GetById(id);
    }
}
