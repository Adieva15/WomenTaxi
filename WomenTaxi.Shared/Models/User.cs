using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenTaxi.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;//храним хеш пароля
        public string PhoneNumber { get; set; }= string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = "Passenger"; //Passenger, Driver, Admin
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //навигационные свойства (для поездок добавим позже)
    }
}
