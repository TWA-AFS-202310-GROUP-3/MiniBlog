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

            userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
        }

        public async Task<User> GetByName(string name)
        {
            return await userCollection.Find(a => a.Name == name).FirstAsync();
        }

        public async Task<User> Create(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await GetByName(user.Name);
        }

    }
}
