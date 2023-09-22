using TokenAuth.Api.Model;

namespace TokenAuth.Api.Repository
{
    public class UserRepository
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin",  Password = "admin",  Role = "Admin"   },
            new User { Id = 2, Username = "user",   Password = "user",   Role = "User"    },
            new User { Id = 3, Username = "jhon",   Password = "jhon",   Role = "Viewer"  },
            new User { Id = 4, Username = "audit",  Password = "audit",  Role = "Audit"   },
            new User { Id = 5, Username = "mary",   Password = "mary",   Role = "User"    },
            new User { Id = 6, Username = "batman", Password = "batman", Role = "User"    },
            new User { Id = 7, Username = "taylor", Password = "taylor", Role = "User"    },
        };

        public static User Get(string username, string password)
        {
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}