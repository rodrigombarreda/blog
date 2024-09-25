using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BackEnd.Data
{
    public class BlogAppDbContext : DbContext
    {
        public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogEntry> BlogsEntrys { get; set; }
        public DbSet<Category> Categories { get; set; }

        
    }
}
