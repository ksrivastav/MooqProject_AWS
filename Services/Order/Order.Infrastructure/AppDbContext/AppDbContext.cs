using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Order.Infrastructure.AppDbContext
{
    public class DaffECommerceDbContext : DbContext
    {
        public DaffECommerceDbContext()
        {

        }
        public DaffECommerceDbContext(DbContextOptions options) : base(options)
        {


        }

        // DB Context Classes
        //public DbSet<User> User { get; set; }
        //public DbSet<UserContact> UserContact { get; set; }
        //public DbSet<UserProfile> UserProfile { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<UserRoleAssoc> UserRoleAssoc { get; set; }
        //public DbSet<Product> Product { get; set; }
        //public DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        //public DbSet<ProductCategory> ProductCategory { get; set; }
        //public DbSet<Payment> Payment { get; set; }
        //public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<Order.Core.Entities.Order> Order { get; set; }
        //public DbSet<OrderStatus> OrderStatus { get; set; }
        //public DbSet<UserCart> UserCart { get; set; }
        //public DbSet<UserClaim> UserClaim { get; set; }
        //public DbSet<RoleClaim> RoleClaim { get; set; }
        //public DbSet<UserToken> UserToken { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            Order.Core.Entities.Order o = new Order.Core.Entities.Order();            
            o.Id = 1;
            o.UserName = "Kartik";
            
            modelBuilder.Entity<Order.Core.Entities.Order>().HasData(o);





        }
    }
}
