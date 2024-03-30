using FBData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FBData.Context
{
    public class FBContext : DbContext
    {

        public DbSet<Player> Players { get; set; }

        public FBContext(DbContextOptions<FBContext> options) : base(options) { }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");

        }
    }
}
