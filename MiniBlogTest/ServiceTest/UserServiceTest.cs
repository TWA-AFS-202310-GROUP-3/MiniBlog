using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Services;
using MiniBlog.Stores;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest
{
    public class UserServiceTest
    {
        private static Mock<IUserRepository> mock = new Mock<IUserRepository>();
        [Fact]
        public async Task Should_create_user_when_invoke_CreateUser_given_input_userAsync()
        {
            //Given
            ArticleStore articleStore = new ArticleStore();
            UserStore userStore = new UserStore();
            var newUser = new User("Tom", "3@3.com");
            mock.Setup(repository => repository.CreateUser(newUser))
                .Returns(Task.FromResult(newUser));
            UserService userService = new UserService(articleStore, userStore, mock.Object);

            //When
            User addedUser = await userService.CreateUserAsync(newUser);

            //Then
            mock.Verify(repository => repository.CreateUser(newUser), Times.Once());
        }

        [Fact]
        public void Should_return_user_when_invoke_GetByName_given_user_name()
        {
            //Given
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            var userSertice = new UserService(articleStore, userStore, mock.Object);

            //Then
            var getUser = userSertice.GetUser("Andrew");

            //Then
            mock.Verify(repository => repository.GetUser("Andrew"), Times.Once());
        }

        [Fact]
        public void Should_return_all_user_when_invoke_GetAll()
        {
            //Given
            var articleStore = new ArticleStore();
            var userStore = new UserStore();
            var userSertice = new UserService(articleStore, userStore, mock.Object);

            //Then
            var users = userSertice.GetAllAsync();

            //Then
            mock.Verify(repository => repository.GetAllUsers(), Times.Once());
        }

        //[Fact]
        //public void Should_return_updated_user_when_invoke_Update()
        //{
        //    //Given
        //    var articleStore = new ArticleStore();
        //    var userStore = new UserStore();
        //    var userService = new UserService(articleStore, userStore, mock.Object);
        //    var user = userService.GetByName("Andrew");
        //    user.Email = "4@4.com";

        //    //Then
        //    var updatedUser = userService.Update(user);

        //    //Then
        //    Assert.Equal("Andrew", updatedUser.Name);
        //    Assert.Equal(user.Email, updatedUser.Email);
        //}

        //[Fact]
        //public void Should_return_deleted_user_when_invoke_Delete()
        //{
        //    //Given
        //    var articleStore = new ArticleStore();
        //    var userStore = new UserStore();
        //    var userService = new UserService(articleStore, userStore, mock.Object);
        //    var userCountBeforeDelete = userService.GetAllAsync().Count();

        //    //Then
        //    var deletedUser = userService.Delete("Andrew");

        //    //Then
        //    Assert.Equal(userCountBeforeDelete - 1, userService.GetAllAsync().Count());
        //    Assert.Equal("Andrew", deletedUser.Name);
        //}
    }
}
