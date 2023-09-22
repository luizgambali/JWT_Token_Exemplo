using TokenAuth.Api.Model;
using TokenAuth.Api.Repository;

namespace TokenAuth.Api.Service
{
    public class LoginService
    {
        public static (int code, User user, string token) Login(User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
    
            if (user == null)
                return (401, null, null);

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return (200, user, token);
        }
    }
}