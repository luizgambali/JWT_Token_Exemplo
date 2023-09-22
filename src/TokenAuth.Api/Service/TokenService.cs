using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TokenAuth.Api.Model;

namespace TokenAuth.Api.Service
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();                       //para gerar o token
            var key = Encoding.ASCII.GetBytes(Settings.Secret);                     //chave de segurança, convertida em array de bytes
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]                            //claims do usuário
                {
                    new Claim(type:ClaimTypes.Name, user.Username.ToString()),
                    new Claim(type:ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Id", user.Id.ToString())
                }),
                
                Expires = DateTime.UtcNow.AddHours(2),                              //expira em 2 horas

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                                                            SecurityAlgorithms.HmacSha256Signature) // chave criptografada
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);                  //cria o token
            return tokenHandler.WriteToken(token);                                  //retorna o token
        }
    }
}