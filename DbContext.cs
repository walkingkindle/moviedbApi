using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieDbApi
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-OKOO9OV\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
