using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;
using System;

namespace SD_Hotel.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SD_HotelDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Rooms.Any())
                return;

            var rooms = new Room[]
            {
                new Room { RoomNumber = "101", Floor = 1, RoomType = RoomType.Single, BasePrice = 150.00m, Description = "Tek kişilik oda", Capacity = 1 },
                new Room { RoomNumber = "102", Floor = 1, RoomType = RoomType.Double, BasePrice = 200.00m, Description = "Çift kişilik oda", Capacity = 2 },
                new Room { RoomNumber = "201", Floor = 2, RoomType = RoomType.Double, BasePrice = 220.00m, Description = "Çift kişilik oda", Capacity = 2 },
                new Room { RoomNumber = "202", Floor = 2, RoomType = RoomType.KingSuite, BasePrice = 350.00m, Description = "Kral dairesi", Capacity = 4 },
                new Room { RoomNumber = "301", Floor = 3, RoomType = RoomType.Single, BasePrice = 160.00m, Description = "Tek kişilik oda", Capacity = 1 },
                new Room { RoomNumber = "302", Floor = 3, RoomType = RoomType.Double, BasePrice = 210.00m, Description = "Çift kişilik oda", Capacity = 2 }
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer { FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet@email.com", Phone = "05551234567", IdentityNumber = "12345678901", Address = "İstanbul", City = "İstanbul", Country = "Türkiye", IsLoyaltyMember = true, LoyaltyPoints = 100 },
                new Customer { FirstName = "Ayşe", LastName = "Demir", Email = "ayse@email.com", Phone = "05559876543", IdentityNumber = "98765432109", Address = "Ankara", City = "Ankara", Country = "Türkiye", IsLoyaltyMember = false, LoyaltyPoints = 0 },
                new Customer { FirstName = "Mehmet", LastName = "Kaya", Email = "mehmet@email.com", Phone = "05551112233", IdentityNumber = "11122233344", Address = "İzmir", City = "İzmir", Country = "Türkiye", IsLoyaltyMember = true, LoyaltyPoints = 250 }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee { FirstName = "Fatma", LastName = "Özkan", IdentityNumber = "11111111111", Email = "fatma@hotel.com", Phone = "05550000001", Address = "Kemer", Role = EmployeeRole.Receptionist, HourlyRate = 25.00m, HireDate = DateTime.Now.AddYears(-2) },
                new Employee { FirstName = "Ali", LastName = "Çelik", IdentityNumber = "22222222222", Email = "ali@hotel.com", Phone = "05550000002", Address = "Kemer", Role = EmployeeRole.Housekeeper, HourlyRate = 20.00m, HireDate = DateTime.Now.AddYears(-1) },
                new Employee { FirstName = "Zeynep", LastName = "Arslan", IdentityNumber = "33333333333", Email = "zeynep@hotel.com", Phone = "05550000003", Address = "Kemer", Role = EmployeeRole.Chef, HourlyRate = 30.00m, HireDate = DateTime.Now.AddYears(-3) },
                new Employee { FirstName = "Mustafa", LastName = "Yıldız", IdentityNumber = "44444444444", Email = "mustafa@hotel.com", Phone = "05550000004", Address = "Kemer", Role = EmployeeRole.Manager, HourlyRate = 0.00m, MonthlySalary = 8000.00m, HireDate = DateTime.Now.AddYears(-5) }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                new Reservation { CustomerId = 1, RoomId = 1, CheckInDate = DateTime.Today.AddDays(1), CheckOutDate = DateTime.Today.AddDays(3), NumberOfGuests = 1, PackageType = PackageType.FullBoard, TotalPrice = 600.00m, Status = ReservationStatus.Confirmed },
                new Reservation { CustomerId = 2, RoomId = 2, CheckInDate = DateTime.Today.AddDays(2), CheckOutDate = DateTime.Today.AddDays(5), NumberOfGuests = 2, PackageType = PackageType.AllInclusive, TotalPrice = 1200.00m, Status = ReservationStatus.Pending },
                new Reservation { CustomerId = 3, RoomId = 4, CheckInDate = DateTime.Today.AddDays(7), CheckOutDate = DateTime.Today.AddDays(10), NumberOfGuests = 3, PackageType = PackageType.AllInclusive, TotalPrice = 1800.00m, Status = ReservationStatus.Confirmed }
            };

            context.Reservations.AddRange(reservations);
            context.SaveChanges();
        }
    }
} 