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
    public interface IStudentsService 
    {
        int Insert(Student s);
        int Update(Student s);
        int Delete(int id);
        Student GetDetail(int id);
        IEnumerable<Student> GetAll();
        Student GetByName(string FirstName, string LastName);
    }

    public class StudentsService : IStudentsService
    {
        MVCWebAppDbContext _context;

        public StudentsService(MVCWebAppDbContext context)
        {
            _context = context;
        }

        public int Delete(int id)
        {
            var objToDelete = _context.Students.Find(id);

            if (objToDelete != null)
            {
                _context.Students.Remove(objToDelete);
                return _context.SaveChanges();
            }

            return -1;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students
                    .OrderByDescending(o => o.StudentID);
        }

        public Student GetByName(string FirstName, string LastName)
        {
            return _context.Students.SingleOrDefault(s => s.FirstName == FirstName && s.LastName == LastName);
        }

        public Student GetDetail(int id)
        {
            return _context.Students.Find(id);
        }

        public int Insert(Student s)
        {
            _context.Students.Add(s);
            return _context.SaveChanges();
        }

        public int Update(Student s)
        {
            s.DateModified = DateTime.Now;
            _context.Entry(s).State = EntityState.Modified;
            _context.Entry(s).Property(x => x.DateCreated).IsModified = false;
            return _context.SaveChanges();
        }
    }
}