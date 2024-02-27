using BiblioTech.API.Entities;
using BiblioTech.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BiblioTech.API.Persistence
{
    public class BiblioTechDbContext : DbContext
    {
        public BiblioTechDbContext(DbContextOptions<BiblioTechDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <BookLoan> BookLoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
