using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_WebApp_With_TDD.Models;

namespace MVC_WebApp_With_TDD.Services
{
    public class StudentsServiceHardcoding : IStudentsService
    {
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();
            students.Add(new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now });
            students.Add(new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1) });

            return students;
        }

        public Student GetByName(string FirstName, string LastName)
        {
            throw new NotImplementedException();
        }

        public Student GetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Student s)
        {
            throw new NotImplementedException();
        }

        public int Update(Student s)
        {
            throw new NotImplementedException();
        }
    }
}