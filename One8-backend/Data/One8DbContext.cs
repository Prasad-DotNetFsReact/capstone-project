using Microsoft.EntityFrameworkCore;
using One8_backend.Models;

namespace One8_backend.Data
{
    public class One8DbContext : DbContext
    {
        public One8DbContext(DbContextOptions<One8DbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<MenuItem>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => !oi.IsDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(rv => !rv.IsDeleted);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Customer" },
                new Role { RoleId = 3, RoleName = "Moderator" },
                new Role { RoleId = 4, RoleName = "deliveryPartner" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 100,
                    Username = "prasadadmin",
                    Password = "prasadadmin",
                    RoleId = 1
                },
                new User
                {
                    Id = 101,
                    Username = "prasadmod",
                    Password = "prasadmod",
                    RoleId = 3
                },
                new User
                {
                    Id = 102,
                    Username = "prasadcust",
                    Password = "prasadcust",
                    RoleId = 2
                },
                new User
                {
                     Id = 103,
                     Username = "delivery",
                     Password = "delivery",
                     RoleId = 4
                }
            );
        }
    }
}
