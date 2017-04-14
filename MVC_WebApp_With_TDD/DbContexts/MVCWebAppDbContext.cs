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
    }
}