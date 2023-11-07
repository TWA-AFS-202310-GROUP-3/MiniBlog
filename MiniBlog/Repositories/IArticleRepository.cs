using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IArticleRepository
    {
        public Task<List<Article>> GetArticles();
        public Task<Article> CreateArticle(Article article);
        public Task<Article> GetArticleById(string id);
        public Task<long> DeleteArticleByName(string name);
    }
}
