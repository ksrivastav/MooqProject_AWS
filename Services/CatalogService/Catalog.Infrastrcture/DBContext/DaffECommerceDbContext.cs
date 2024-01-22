using Catalog.Core.Entities;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Catalog.Infrastrcture.DBContext
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
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        //public DbSet<Payment> Payment { get; set; }
        //public DbSet<PaymentStatus> PaymentStatus { get; set; }
        //public DbSet<Order> Order { get; set; }
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


            string prodCat = File.ReadAllText("ProductCategory.json");
            List<ProductCategory> listProdCat = System.Text.Json.JsonSerializer.Deserialize<List<ProductCategory>>(prodCat);
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");

            foreach (var item in listProdCat)
            {
                modelBuilder.Entity<ProductCategory>().HasData(item);
            }



            string prodSubCat = File.ReadAllText("ProductSubCategory.json");
            List<ProductSubCategory> listProdSubCat = System.Text.Json.JsonSerializer.Deserialize<List<ProductSubCategory>>(prodSubCat);
            modelBuilder.Entity<ProductSubCategory>().ToTable("ProductSubCategory");
            foreach (var item in listProdSubCat)
            {
                modelBuilder.Entity<ProductSubCategory>().HasData(item);
            }




            string prod = File.ReadAllText("Product.json");
            List<Product> listProd = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(prod);
            modelBuilder.Entity<Product>().ToTable("Product");
            foreach (var item in listProd)
            {
                modelBuilder.Entity<Product>().HasData(item);
            }



        }
    }
}
