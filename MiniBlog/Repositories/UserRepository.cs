using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MiniBlog.Model;
using MongoDB.Driver;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;
        public UserRepository(IMongoClient mongoClient)
        {
            userCollection = mongoClient.GetDatabase("MiniBlog").GetCollection<User>(User.CollectionName);
        }

        public async Task<User> CreateUser(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await GetUserByName(user.Name);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await userCollection.Find(user => user.Name == userName).FirstOrDefaultAsync();
        }
    }
}
