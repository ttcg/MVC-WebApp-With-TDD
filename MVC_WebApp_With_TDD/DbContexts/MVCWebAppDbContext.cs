using MVC_WebApp_With_TDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MVC_WebApp_With_TDD.DbContexts
{
    public class MVCWebAppDbContext : DbContext
    {
        public MVCWebAppDbContext() : base("MVCWebAppDbContext")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student2> Students2 { get; set; }
        public virtual DbSet<Campus> Campuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campus>()
                    .ToTable("Campuses", "dbo");

            modelBuilder.Entity<Student2>()
                    .ToTable("Students2", "dbo");
        }
    }
}