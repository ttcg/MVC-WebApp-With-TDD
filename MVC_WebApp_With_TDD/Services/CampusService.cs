using MVC_WebApp_With_TDD.DbContexts;
using MVC_WebApp_With_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace MVC_WebApp_With_TDD.Services
{
    public interface ICampusService 
    {        
        IEnumerable<Campus> GetAll();        
    }

    public class CampusService : ICampusService
    {
        MVCWebAppDbContext _context;

        public CampusService(MVCWebAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Campus> GetAll()
        {
            return _context.Campuses
                    .OrderByDescending(o => o.CampusName);
        }

        
    }
}