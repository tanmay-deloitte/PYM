using Microsoft.EntityFrameworkCore;
using PYM.models;
using PYM.Models;

namespace PYM;
public class PYMContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Label> Label { get; set; }

        public PYMContext(DbContextOptions options):base(options)
        {
            
        }
}