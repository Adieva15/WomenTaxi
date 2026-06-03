using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenTaxi.Shared.Dtos
{
    public class AuthResponseDto
    {
        //ответ после логина/регистрации
        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public String Role { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
    }
}
