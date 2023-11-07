using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace MiniBlogTest.ControllerTest
{
    [Collection("IntegrationTest")]
    public class ArticleControllerTest : TestBase
    {
        private static Mock<IArticleRepository> mock = new Mock<IArticleRepository>();
        private static Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();

        public ArticleControllerTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void Should_get_all_Article()
        {
            mock.Setup(repository => repository.GetAllArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }));
            var client = GetClient(new ArticleStore(), new UserStore(new List<User>()), mock.Object);
            var response = await client.GetAsync("/article");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Article>>(body);
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async void Should_create_article_fail_when_ArticleStore_unavailable()
        {
            string userNameWhoWillAdd = "Tom";
            string articleContent = "What a good day today!";
            string articleTitle = "Good day";
            Article article = new Article(userNameWhoWillAdd, articleTitle, articleContent);
            mock.Setup(repository => repository.CreateArticle(article))
                .Returns(Task.FromResult(article));
            var client = GetClient(null, new UserStore(new List<User>()), mock.Object);

            var httpContent = JsonConvert.SerializeObject(article);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/article", content);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async void Should_create_article_and_register_user_correct()
        {
            string userNameWhoWillAdd = "Tom";
            string articleContent = "What a good day today!";
            string articleTitle = "Good day";

            Article article = new Article(userNameWhoWillAdd, articleTitle, articleContent);
            User newUser = new User(userNameWhoWillAdd);
            mock.Setup(repository => repository.CreateArticle(article))
    .Returns(Task.FromResult(article));
            mockUserRepo.Setup(repository => repository.GetUser("Tom"))
    .Returns(Task.FromResult<User>(null));
            mockUserRepo.Setup(repository => repository.CreateUser(newUser))
                .Returns(Task.FromResult<User>(newUser));

            mock.Setup(repo => repo.GetAllArticles())
                     .ReturnsAsync(
                        new List<Article>
                        {
                                new Article("Tom", "Good day", "What a good day today!"),
                                new Article("Tom", "Good day", "What a good day today!"),
                        });
            var client = GetClient(new ArticleStore(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }), new UserStore(new List<User>()), mock.Object, mockUserRepo.Object);

            var httpContent = JsonConvert.SerializeObject(article);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var createArticleResponse = await client.PostAsync("/article", content);

            // It fail, please help
            Assert.Equal(HttpStatusCode.Created, createArticleResponse.StatusCode);

            var articleResponse = await client.GetAsync("/article");
            var body = await articleResponse.Content.ReadAsStringAsync();
            var articles = JsonConvert.DeserializeObject<List<Article>>(body);
            Assert.Equal(2, articles.Count);
            Assert.Equal(articleTitle, articles[1].Title);
            Assert.Equal(articleContent, articles[1].Content);
            Assert.Equal(userNameWhoWillAdd, articles[1].UserName);

            var userResponse = await client.GetAsync("/user");
            var usersJson = await userResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            //Assert.Equal(1, users.Count);
            //Assert.Equal(userNameWhoWillAdd, users[0].Name);
            //Assert.Equal("anonymous@unknow.com", users[0].Email);
        }
    }
}
