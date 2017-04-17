using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MVC_WebApp_With_TDD.Services;
using MVC_WebApp_With_TDD.Models;
using System.Data.Entity;

namespace MVC_WebApp_With_TDD.Tests
{
    public class StudentsServiceTestFixture 
    {
        public IStudentsService StudentService;
        public Mock<DbContexts.MVCWebAppDbContext> MVCWebAppDbContext;
        //public List<Student> Students { get; private set; }

        public StudentsServiceTestFixture()
        {
            MVCWebAppDbContext = new Mock<DbContexts.MVCWebAppDbContext>();

            StudentService = new StudentsService(MVCWebAppDbContext.Object);

            var campus = new Campus() { CampusID = 1, CampusName = "SMF" };
            var Students = new List<Student>()
            {
                new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-9), DateCreated = DateTime.Now, CampusID = 1, Campus = campus },
                new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1), CampusID = 2, Campus = campus }
            };

            // set up student entity.
            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(Students.AsQueryable().Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(Students.AsQueryable().Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(Students.AsQueryable().ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(Students.AsQueryable().GetEnumerator());

            MVCWebAppDbContext.Setup(c => c.Students).Returns(mockSet.Object);            

            //MVCWebAppDbContext.Setup(m => m.Students.Include("Campus")).Returns(mockSet.Object);
        }      
    }
}
