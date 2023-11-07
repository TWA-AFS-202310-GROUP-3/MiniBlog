using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest;

public class ArticleServiceTest
{
    [Fact]
    public async Task Should_create_article_when_invoke_CreateArticle_given_input_article()
    {
        // given
        var newArticle = new Article("Jerry", "Let's code", "c#");
        var newUser = new User(newArticle.UserName);
        var mockArticle = new Mock<IArticleRepository>();
        mockArticle.Setup(r => r.CreateArticle(newArticle)).Returns(Task.FromResult(newArticle));
        var mockUser = new Mock<IUserRepository>();
        mockUser.Setup(r => r.GetUserByName(newArticle.UserName)).Returns(Task.FromResult<User>(null));
        var articleService = new ArticleService(mockArticle.Object, mockUser.Object);
        var addedArticle = await articleService.CreateArticle(newArticle);
        Assert.Equal(newArticle.Title, addedArticle.Title);
        Assert.Equal(newArticle.Content, addedArticle.Content);
        Assert.Equal(newArticle.UserName, addedArticle.UserName);

        mockArticle.Verify(m => m.CreateArticle(newArticle));
        mockUser.Verify(m => m.GetUserByName(newArticle.UserName));
    }

    [Fact]
    public async Task Should_get_article_when_GetById_given_valid_id()
    {
        var articleService = new ArticleService();
        var id = Guid.NewGuid().ToString();
        var expectedArticle = new Article("Jerry", "Let's code", "c#");
        var mockArticle = new Mock<IArticleRepository>();
        var mockUser = new Mock<IUserRepository>();
        mockArticle.Setup(repo => repo.GetArticleById(id)).ReturnsAsync(expectedArticle);

        //when
        var article = await articleService.GetArticleById(id);
        //then
        Assert.Equal(expectedArticle, article);
        mockArticle.Verify(repo => repo.GetArticleById(id));
    }
}
