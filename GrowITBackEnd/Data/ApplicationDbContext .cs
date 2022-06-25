using GrowITBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models.AuthModels;

namespace GrowITBackEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //reference to every Model        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_Items> Cart_Items { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order_Items> Order_Items { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Support_Tickets> Support_tickets { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Wishlist_Items> Wishlist_Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //additional properties for user table
            builder.Entity<ApplicationUser>()
               .Property(e => e.FistName)
               .HasMaxLength(250);

            builder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Address)
                .HasMaxLength(250);

            //composite key for Order_Items
            builder.Entity<Order_Items>()
               .HasKey(c => new { c.OrdersID, c.ItemID });


            //composite key for Cart_Items
            builder.Entity<Cart_Items>()
               .HasKey(c => new { c.ItemID, c.CartID });

            //composite key for wishlist_Items
            builder.Entity<Wishlist_Items>()
              .HasKey(c => new { c.WishID, c.ItemID });
            
        }
    }
}