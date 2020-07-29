using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace BookStore.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookStore") { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasMany(b => b.Purchases)
                .WithMany(p => p.Books)
                .Map(t => t.MapLeftKey("Id")
                .MapRightKey("PurchaseId")
                .ToTable("BookPurchase"));
        }
    }
}