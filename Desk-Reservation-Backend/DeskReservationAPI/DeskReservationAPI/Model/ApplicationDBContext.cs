
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
       .HasValue<Fixreservation>("fix");

            modelBuilder.Entity<Office>().HasData(
                 GenerateRundomOffices(5)

                );

            modelBuilder.Entity<Desk>().HasData(
                 GenerateRundomDesks(50, 5)
                );

            modelBuilder.Entity<Role>().HasData
                (
                new Role { RoleID = 1, Name = "user" },
                new Role { RoleID = 2, Name = "admin" }
                );



            PasswordEncoder pswEncoder = new PasswordEncoder();

            modelBuilder.Entity<User>().HasData(
                new User { UserID = Guid.NewGuid(), FirstName = "user", LastName = "user", Email = "user@gmail.com", Department = "dep", Password = pswEncoder.Encode("user"), RoleID = 1 }
                );

            modelBuilder.Entity<User>().HasData(
               new User { UserID = Guid.NewGuid(), FirstName = "admin", LastName = "admin", Email = "admin@gmail.com", Department = "dep", Password = pswEncoder.Encode("admin"), RoleID = 2 }
               );

            modelBuilder.Entity<Equipment>().HasData
                (
                  new Equipment {EquipmentID = 1, Feature="socet"},
                  new Equipment { EquipmentID = 2,Feature ="screen"}
                );

        }

        private IEnumerable<Office> GenerateRundomOffices(int count)
        {
            List<Office> offices = new List<Office>();
            for (int i = 0; i < count; i++)
            {
                Office office = new Office { OfficeID = i + 1, Name = $"office_{i + 1}" };
                offices.Add(office);
            }
            return offices;

        }

        private IEnumerable<Desk> GenerateRundomDesks(int desksCount, int officesCount)
        {
            List<Desk> desks = new List<Desk>();
            int quote = 0;
            quote = desksCount % officesCount == 0 ? desksCount / officesCount : (desksCount / officesCount) + 1;
            for (int i = 0; i < desksCount; i++)
            {
                int officeID = (i / quote) + 1;
                Desk desk = new Desk { DeskID = i + 1, Label = $"desk_{i + 1}", Map = "default.png", OfficeID = officeID };
                desks.Add(desk);
            }
            return desks;
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Office> Offices { get; set; }
        public DbSet<FlexReservation> FlexReservations { get; set; }
        public DbSet<Fixreservation> FixReservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
