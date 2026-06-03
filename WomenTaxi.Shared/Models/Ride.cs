using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WomenTaxi.Shared.Models
{
    public class Ride
    {
        // начальная версия 
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int? DriverId { get; set; } // nullable, пока не назначен
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public double FromLat { get; set; } // широта отправления
        public double FromLng { get; set; } // долгота отправления
        public double ToLat { get; set; } 
        public double ToLng { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Watching";// Waiting, DriverFound, InProgress, Completed, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? Completed { get; set; }

        //Навигационные свойства (связи с пользователями)
        public User? Passenger { get; set; }
        public User? Driver { get; set; }
    }
}
