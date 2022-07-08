using ContactAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.DataContext
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <Contact> contacts { get; set; }
    }
}
