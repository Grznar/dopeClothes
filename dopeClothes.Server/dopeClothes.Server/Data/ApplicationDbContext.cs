using dopeClothes.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace dopeClothes.Server.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<Order>()
                .HasOne(o => o.ShoppingCart)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public virtual DbSet<Product>Products { get; set; }
        public virtual DbSet<WhishList> WhishLists { get; set; }
        public virtual DbSet<WishListItem> WishListItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        
    }
}
