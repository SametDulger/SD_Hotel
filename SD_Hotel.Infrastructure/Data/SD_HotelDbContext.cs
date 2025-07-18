using Microsoft.EntityFrameworkCore;
using SD_Hotel.Core.Entities;

namespace SD_Hotel.Infrastructure.Data
{
    public class SD_HotelDbContext : DbContext
    {
        public SD_HotelDbContext(DbContextOptions<SD_HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<ExtraExpense> ExtraExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoomNumber).IsRequired().HasMaxLength(10);
                entity.Property(e => e.BasePrice).HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.RoomNumber).IsUnique();
            });

            modelBuilder.Entity<RoomImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.Room).WithMany(e => e.RoomImages).HasForeignKey(e => e.RoomId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.IdentityNumber).HasMaxLength(20);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EarlyReservationDiscount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Customer).WithMany(e => e.Reservations).HasForeignKey(e => e.CustomerId);
                entity.HasOne(e => e.Room).WithMany(e => e.Reservations).HasForeignKey(e => e.RoomId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.IdentityNumber).HasMaxLength(20);
                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MonthlySalary).HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HoursWorked).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Employee).WithMany(e => e.Shifts).HasForeignKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<Overtime>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalPay).HasColumnType("decimal(18,2)");
                entity.Property(e => e.HoursWorked).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Employee).WithMany(e => e.Overtimes).HasForeignKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<ExtraExpense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Customer).WithMany(e => e.ExtraExpenses).HasForeignKey(e => e.CustomerId);
                entity.HasOne(e => e.Reservation).WithMany().HasForeignKey(e => e.ReservationId);
            });
        }
    }
} 