using System.Collections.Generic;

namespace MiniBlog.Model
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email = "anonymous@unknow.com")
        {
            this.Name = name;
            this.Email = email;
        }

        public static string CollectionName { get; set; } = "user";

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
