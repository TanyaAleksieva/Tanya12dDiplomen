using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FleksTanya12d.Data
{
    public class FleksDbContext : IdentityDbContext<User>
    {
        public FleksDbContext(DbContextOptions<FleksDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sport> Sports { get; set; }
    }
}