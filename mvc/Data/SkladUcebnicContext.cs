#nullable disable
using Microsoft.EntityFrameworkCore;
using SkladUcebnic.Models;

namespace SkladUcebnic.Data
{
    public class SkladUcebnicContext : DbContext
    {
        public SkladUcebnicContext (DbContextOptions<SkladUcebnicContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<BookOrder> BookOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary key
            modelBuilder
                .Entity<BookOrder>()
                .HasKey(x => new { x.BookId, x.OrderId });
        }
    }
}
