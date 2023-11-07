using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IMongoCollection<Article> articleCollection;
        public ArticleRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDataBase = mongoClient.GetDatabase("MiniBlog");
            articleCollection = mongoDataBase.GetCollection<Article>(Article.CollectionName);
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await articleCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Article> CreateArticle(Article article)
        {
            var newArticle = new Article(article.UserName, article.Title, article.Content);
            await articleCollection.InsertOneAsync(newArticle);
            return await articleCollection.Find(a => a.Title == article.Title).FirstAsync();
        }

        public async Task<Article> GetArticle(string id)
        {
            return await articleCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }
    }
}
