using MiniBlog.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;
        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("MiniBlog");
            userCollection = mongoDatabase.GetCollection<User>(User.UserCollectionName);
        }

        public async Task<User> CreateUser(User user)
        {
            await userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var foundUser = await userCollection.Find(_ => _.Name == userName).FirstOrDefaultAsync();
            return foundUser;
        }
    }
}
