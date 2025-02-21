using AssignmentOfNimap.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AssignmentOfNimap.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {

        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
