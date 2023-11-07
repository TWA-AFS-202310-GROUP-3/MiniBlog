using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    private readonly Mock<IArticleRepository> mockArticleRepository;
    private readonly Mock<IUserRepository> mockUserRepository;
    private readonly ArticleService articleService;

    public ArticleServiceTest()
    {
        mockArticleRepository = new Mock<IArticleRepository>();
        mockUserRepository = new Mock<IUserRepository>();
        articleService = new ArticleService(mockArticleRepository.Object, mockUserRepository.Object);
    }

    [Fact]
    public async void Should_create_article_when_invoke_CreateArticle_given_input_article()
    {
        // given
        //var newArticle = new Article("Jerry", "Let's code", "c#");
        var newArticle = new Article("xianke", "happy", "csgo");
        mockArticleRepository.Setup(repository => repository.CreateArticle(It.IsAny<Article>())).Callback<Article>(article => article.Id = Guid.NewGuid().ToString()).ReturnsAsync((Article article) => article);
        mockUserRepository.Setup(repository => repository.GetUserByName(It.IsAny<string>())).ReturnsAsync((User)null);
        mockUserRepository.Setup(repository => repository.CreateUser(It.IsAny<User>())).ReturnsAsync((User user) => user);

        //var articleStore = new ArticleStore();
        //var articleCountBeforeAddNewOne = articleStore.Articles.Count;
        //var userStore = new UserStore();
        //var articleService = new ArticleService(articleStore, userStore);

        // when
        var addedArticle = await articleService.CreateArticle(newArticle);

        // then
        Assert.Equal(newArticle.Title, addedArticle.Title);
        Assert.Equal(newArticle.Content, addedArticle.Content);
        Assert.Equal(newArticle.UserName, addedArticle.UserName);
    }
}
