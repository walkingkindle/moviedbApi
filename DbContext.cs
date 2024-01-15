using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieDbApi
{
    internal class AppDbContext:DbContext
    {
        public DbSet<Show> Shows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //create database file and then start storing everything.
        }
    }
}
