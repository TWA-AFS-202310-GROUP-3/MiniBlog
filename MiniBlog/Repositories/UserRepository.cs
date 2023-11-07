using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;
        public UserRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDataBase = mongoClient.GetDatabase("MiniBlog");
            userCollection = mongoDataBase.GetCollection<User>(User.CollectionName);
        }

        public async Task<User> CreateUser(User user)
        {
            var newUser = new User(user.Name,user.Email);
            await userCollection.InsertOneAsync(newUser);
            return await userCollection.Find(a => a.Name == user.Name).FirstAsync();
        }

        public Task<User> Delete(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUser(string name)
        {
            return await userCollection.Find(user => user.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public Task<User> Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
