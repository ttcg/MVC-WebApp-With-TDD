using MVC_WebApp_With_TDD.DbContexts;
using MVC_WebApp_With_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC_WebApp_With_TDD.Services
{
    public interface IStudentsService
    {
        int Insert(Student s);
        int Update(Student s);
        int Delete(Student s);
        Student GetDetail(int id);
        IEnumerable<Student> GetAll();
    }

    public class StudentsService : IStudentsService
    {
        MVCWebAppDbContext _context;

        public StudentsService(MVCWebAppDbContext context)
        {
            _context = context;
        }

        public int Delete(Student s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Student s)
        {
            _context.Students.Add(s);
            return _context.SaveChanges();
        }

        int IStudentsService.Update(Student s)
        {
            throw new NotImplementedException();
        }
    }
}