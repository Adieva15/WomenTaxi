using Microsoft.EntityFrameworkCore;
using WomenTaxi.Shared.Models;

namespace WomenTaxi.Data
{
    public class AppDbContext: DbContext //так наследуется дочерний класс от DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Настройка связей
            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Passenger)  // У Ride есть один Passenger (пользователь-пассажир)
                .WithMany() // У User может быть много поездок в роли пассажира 
                .HasForeignKey(r => r.PassengerId) // Внешний ключ в таблице Ride — PassengerId
                .OnDelete(DeleteBehavior.Restrict); // При удалении User не удалять связанные поездки, а запретить удаление
            //запретит удаление пользователя, если он участвует в любой поездке 

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Driver)
                .WithMany()
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Уникальный индекс на Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)//Создаёт уникальный индекс в базе данных 
                .IsUnique();

        }
    }
}
