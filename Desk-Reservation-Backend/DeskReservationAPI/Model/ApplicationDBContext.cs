
using DeskReservationAPI.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection.Metadata;
namespace DeskReservationAPI.Model
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Reservation>()
       .HasDiscriminator<string>("ReservationType")
       .HasValue<FlexReservation>("flex")
       .HasValue<FixReservation>("fix");

            modelBuilder.Entity<Office>().HasData(
                SeededData.GenerateRundomOffices(5)

                );

            modelBuilder.Entity<Desk>().HasData(
               SeededData.GenerateRundomDesks(50, 5)
                );

            modelBuilder.Entity<Role>().HasData
                (
              SeededData.UserRole,
              SeededData.AdminRole
                );


            modelBuilder.Entity<User>().HasData(
               SeededData.User1,
               SeededData.User2,
               SeededData.User3,
               SeededData.AdminUser
                );



            modelBuilder.Entity<Equipment>().HasData
                (
          SeededData.SocketEqui,
          SeededData.ScreenEquip
                );

            modelBuilder.Entity<FixReservation>().HasData
                (SeededData.FixReservations

                );

            modelBuilder.Entity<FlexReservation>().HasData
                (
               SeededData.FlexReservations
                );

            modelBuilder.Entity<Comment>().HasData
                (
                  SeededData.Comments
                );

        }






        public DbSet<Comment> Comments { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Office> Offices { get; set; }
        public DbSet<FlexReservation> FlexReservations { get; set; }
        public DbSet<FixReservation> FixReservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
