using Microsoft.EntityFrameworkCore;
using RentCaarsAPIs.Models;
using System.Collections.Generic;

namespace RentCaarsAPIs.Data {  
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
      
    }
}
