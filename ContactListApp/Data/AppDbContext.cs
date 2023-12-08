using ContactListApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database will be created and seeded when the application starts
            DbInitializer.Initialize(this);
        }
    }
}
