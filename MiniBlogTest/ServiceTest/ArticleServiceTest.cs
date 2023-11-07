using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest
{
    public class ArticleServiceTest
    {
        private static Mock<IArticleRepository> mockArticleRepo = new Mock<IArticleRepository>();
        private static Mock<IUserRepository> mockUserRepo = new Mock<IUserRepository>();

        [Fact]
        public async void Should_create_article_when_invoke_CreateArticle_given_input_article()
        {
            var newArticle = new Article("Tom", "Let's stop code", "c");
            var articleStore = new ArticleStore();
            var newUser = new User(newArticle.UserName);
            mockArticleRepo.Setup(repository => repository.CreateArticle(newArticle))
                .Returns(Task.FromResult(newArticle));
            mockUserRepo.Setup(repository => repository.GetUser(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(null));
            mockUserRepo.Setup(repository => repository.CreateUser(It.IsAny<User>()))
                .Returns(Task.FromResult<User>(newUser));
            var userStore = new UserStore();
            var articleService = new ArticleService(articleStore, userStore, mockArticleRepo.Object, mockUserRepo.Object);

            var addedArticle = await articleService.CreateArticleAsync(newArticle);

            mockUserRepo.Verify(repository => repository.GetUser(It.IsAny<string>()), Times.Once());
            mockUserRepo.Verify(repository => repository.CreateUser(It.IsAny<User>()), Times.Once());
            mockArticleRepo.Verify(repository => repository.CreateArticle(newArticle), Times.Once());
        }

        [Fact]
        public async void Should_create_article_without_create_user_when_invoke_CreateArticle_given_input_article()
        {
            var newArticle = new Article("Tom", "Let's stop code", "c");
            var articleStore = new ArticleStore();
            var newUser = new User("Tom");
            mockArticleRepo.Setup(repository => repository.CreateArticle(newArticle))
                .Returns(Task.FromResult(newArticle));
            mockUserRepo.Setup(repository => repository.GetUser("Tom"))
                .Returns(Task.FromResult<User>(newUser));
            var userStore = new UserStore();
            var articleService = new ArticleService(articleStore, userStore, mockArticleRepo.Object, mockUserRepo.Object);

            var addedArticle = await articleService.CreateArticleAsync(newArticle);

            mockArticleRepo.Verify(repository => repository.CreateArticle(newArticle), Times.Once());
        }

        [Fact]
        public async Task Should_return_article_when_invoke_GetByID_given_article_IDAsync()
        {
            //Given
            var newArticle = new Article("Tom", "Let's stop code", "c");
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            mockArticleRepo.Setup(repository => repository.GetArticle("Id"))
                .Returns(Task.FromResult(newArticle));
            var articleService = new ArticleService(articleStore, userStore, mockArticleRepo.Object, mockUserRepo.Object);

            //Then
            var getArticle = await articleService.GetByIdAsync("Id");

            //Then
            mockArticleRepo.Verify(repository => repository.GetArticle("Id"), Times.Once());
        }

        [Fact]
        public async Task Should_return_all_article_when_invoke_GetAllAsync()
        {
            //Given
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            mockArticleRepo.Setup(repository => repository.GetAllArticles()).Returns(Task.FromResult(new List<Article>
            {
                new Article(null, "Happy new year", "Happy 2021 new year"),
                new Article(null, "Happy Halloween", "Halloween is coming"),
            }));
            var articleService = new ArticleService(articleStore, userStore, mockArticleRepo.Object, mockUserRepo.Object);

            //Then
            var getArticle = await articleService.GetAllAsync();

            //Then
            mockArticleRepo.Verify(repository => repository.GetAllArticles(), Times.Once());
        }
    }
}