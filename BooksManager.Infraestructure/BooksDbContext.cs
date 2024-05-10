using BooksManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BooksManager.Infraestructure
{
    public class BooksDbContext(DbContextOptions<BooksDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Book> Lendings { get; set; }
        public DbSet<Book> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
