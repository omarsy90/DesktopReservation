using DeskReservationAPI.Model;
using DeskReservationAPI.Utility;
using static DeskReservationAPI.Repository.UserRepository;

namespace DeskReservationAPI
{
    public class SeededData
    {
        private static PasswordEncoder pswEncoder = new PasswordEncoder();

        //Users
        public readonly static string User1Pass = "user";
        public readonly static string User2Pass = "user2";
        public  readonly static string AdminUserPass = "admin";
        public readonly static User User1 =  new User() { UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", FirstName = "user", LastName = "user", Email = "user@gmail.com", Department = "dep", Password = pswEncoder.Encode(User1Pass), RoleID = 1 } ;
        public readonly static User User2 = new User { UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", FirstName = "user2", LastName = "user2", Email = "user2@gmail.com", Department = "dep", Password = pswEncoder.Encode(User2Pass), RoleID = 1 };
        public readonly static User AdminUser = new User { UserID = "66AFEF1E-0253-45F4-9968-6072073AD6C6", FirstName = "admin", LastName = "admin", Email = "admin@gmail.com", Department = "dep", Password = pswEncoder.Encode(AdminUserPass), RoleID = 2 };

        //Role
        public readonly static Role UserRole = new Role { RoleID = 1, Name = RoleName.NormalUser };
        public readonly static Role AdminRole = new Role { RoleID = 2, Name =RoleName.Admin};

        //Equipments
        public readonly static Equipment SocketEqui = new Equipment { EquipmentID = 1, Feature = "socket" };
        public readonly static Equipment ScreenEquip = new Equipment { EquipmentID = 2, Feature = "screen" };

        public readonly static List<FixReservation> FixReservations = new List<FixReservation>
        {
            new FixReservation { ReservationID = 1, UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", DeskID = 1, DateStart = DateTime.Parse("2024-10-01"), DateEnd = DateTime.Parse("2024-10-01").AddDays(90-1), IsConfirmed = true },
                  new FixReservation { ReservationID = 2, UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", DeskID = 1, DateStart = DateTime.Parse("2023-10-01"), DateEnd = DateTime.Parse("2023-10-01").AddDays(90-1), IsConfirmed = true },
                  new FixReservation { ReservationID = 3, UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", DeskID = 1, DateStart = DateTime.Parse("2024-03-01"), DateEnd = DateTime.Parse("2024-03-01").AddDays(90-1), },
                  new FixReservation { ReservationID = 7, UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", DeskID = 1, DateStart = DateTime.Parse("2024-12-01"), DateEnd = DateTime.Parse("2024-12-01").AddDays(90 - 1), }
        };

        public readonly static List<FlexReservation> FlexReservations = new List<FlexReservation>
        {
              new FlexReservation {ReservationID=4,UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",DeskID=2, DateStart = DateTime.Parse("2024-03-03"),DateEnd=DateTime.Parse("2024-03-05"), },
                 new FlexReservation { ReservationID =5,UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", DeskID=2, DateStart = DateTime.Parse("2024-04-03"), DateEnd = DateTime.Parse("2024-04-05") },
                 new FlexReservation { ReservationID=6,UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", DeskID = 3, DateStart = DateTime.Parse("2024-06-03"), DateEnd = DateTime.Parse("2024-06-04") }
        };


        //// Comments
        public readonly static List<Comment> Comments = new List<Comment>
        {
            new Comment{CommentID =1 ,UserID = User1.UserID.ToString() , DeskID = 1 ,CommentTxt="headset not available",CommentedAt= DateTime.Now},
            new Comment{CommentID =2 ,UserID = User1.UserID.ToString() , DeskID = 2 ,CommentTxt="screen should be bigger",CommentedAt= DateTime.Now},
            new Comment{CommentID =3 ,UserID = User2.UserID.ToString() , DeskID = 2 ,CommentTxt="chair should be movable",CommentedAt= DateTime.Now},
            new Comment{CommentID =4 ,UserID = User2.UserID.ToString() , DeskID = 3 ,CommentTxt="good infrastructured",CommentedAt= DateTime.Now}
        };


        public static IEnumerable<Office> GenerateRundomOffices(int count)
        {
            List<Office> offices = new List<Office>();
            for (int i = 0; i < count; i++)
            {
                Office office = new Office { OfficeID = i + 1, Name = $"office_{i + 1}" };
                offices.Add(office);
            }
            return offices;

        }

        public static IEnumerable<Desk> GenerateRundomDesks(int desksCount, int officesCount)
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

    }
}
