using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Snakesssss.Model
{
    public  class SnakeContext : DbContext
    {
        public SnakeContext()
            :base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= C:\poe\snakes.db");
        }
        
        public DbSet<Snake> Snakes { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Family> Families { get; set; }

        public DbSet<SnakeColor> SnakesColors { get; set; }

        public DbSet<PoisonType> PoisonTypes { get; set; }

        public DbSet<Design> Designs { get; set; }
    }
}
