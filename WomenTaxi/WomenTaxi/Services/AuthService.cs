
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WomenTaxi.Shared.Models;
using System.IdentityModel.Tokens.Jwt;

namespace WomenTaxi.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)//Проверяет, соответствует ли введённый пароль сохранённому хешу.
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);// извлекает соль из хеша, вычисляет хеш пароля и сравнивает результат с переданным хешем.
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]//утверждения
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FirstName", user.FirstName)
            };
            //Создание криптографического ключа
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//Указывает, что токен будет подписан с использованием алгоритма HMAC-SHA256.

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],//Издатель токена — обычно URL вашего API 
                audience: _configuration["Jwt:Audience"],//Получатель токена — кто может его использовать
                claims: claims,//Массив утверждений
                expires: DateTime.UtcNow.AddDays(7),//Время жизни токена: текущее время UTC + 7 дней
                signingCredentials: creds);//Учётные данные для подписи, чтобы гарантировать, что токен не был подделан.

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
