using Microsoft.IdentityModel.Tokens;
using PrimeiraApi.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrimeiraApi.Application.Services
{
    public class TokenService
    {
        public static object GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret); // criptografia da chave

            var tokenConfig = new SecurityTokenDescriptor  // configurando o token (payload,
            {
                Subject = new ClaimsIdentity(new Claim[] // criação das Claims
                {
                    new Claim("userId",user.id.ToString()), // primeira clams par chave valor que vai estar no payload
                }),
                Expires = DateTime.UtcNow.AddHours(3), // Tempo de expiração do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // tipo de assinatura, a chave privada e o tipo de cryptografia que fica no Header

            };
            var tokenHandler = new JwtSecurityTokenHandler(); // class para efetivamente gerar o token
            var token = tokenHandler.CreateToken(tokenConfig); // Criar o Token
            var tokenString = tokenHandler.WriteToken(token); // escrever o token literalmente o token

            return new
            {
                token = tokenString
            };



        }
    }
}
