using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WomenTaxi.Data;// Пространство имен DbContext
using WomenTaxi.Shared.Dtos;
using WomenTaxi.Shared.Models;
using WomenTaxi.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WomenTaxi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        //регистрация нового пользователя
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            //Проверка уникальности email
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            //Создание объекта пользователя
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = _authService.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = dto.Role
            };

            //Сохранение в базу данных
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //Генерация JWT-токена и ответ
            var token = _authService.GenerateJwtToken(user);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName
            });
        }

        //вход существующего пользователя
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !_authService.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password");//не сообщаем, что именно неверно (email или пароль) 401

            //возвращаем токен и данные пользователя.
            var token = _authService.GenerateJwtToken(user);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName
            });
        }
    }
}
