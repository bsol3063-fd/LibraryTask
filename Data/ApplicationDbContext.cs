using HomeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HomeLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().ToTable("Books");

            // Id — первичный ключ, генерируется на уровне БД
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}