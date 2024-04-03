using FBData.Interface;
using FBData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        //Further work: Ideally get this to go on a interface, so we can use DI to make to make the app unit testable.
        public DbSet<Player> Players { get; set; }

        public FBContext(DbContextOptions<FBContext> options) : base(options) { }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");

        }

    }
}
