using MVC_WebApp_With_TDD.Models;
using MVC_WebApp_With_TDD.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace MVC_WebApp_With_TDD.Tests
{
    public class StudentsServiceTests
    {
        private StudentsService _studentService;
        private DbContexts.MVCWebAppDbContext _dbContext;
        public StudentsServiceTests()
        {
            _dbContext = new DbContexts.MVCWebAppDbContext();
            _studentService = new StudentsService(_dbContext);
        }

        [Fact]
        public void should_Add_Student()
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {                
                var student = new Student() { RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), Campus = new Campus { CampusID = 1, CampusName = "Test" } };
                _studentService.Insert(student);
                Assert.True(student.StudentID > 0);

                dbContextTransaction.Rollback();
            }
        }

        [Fact]
        public void should_throw_Exception_Add_Student()
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                var student = new Student() { FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

                Assert.Throws<DbEntityValidationException>(() => _studentService.Insert(student));
            }
        }

        [Fact]
        public void should_Update_Student()
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                var student = _studentService.GetDetail(1);

                student.FirstName = "Updated FirstName";
                student.LastName = "Updated Surname";

                _studentService.Update(student);

                Assert.Equal("Updated FirstName", student.FirstName);
                Assert.Equal("Updated Surname", student.LastName);
            }
        }

        [Fact]
        public void should_throw_Exception_Update_Student()
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                var student = _studentService.GetDetail(1);

                student.RefNo = null;

                Assert.Throws<DbEntityValidationException>(() => _studentService.Update(student));
            }
        }

        [Fact]
        public void should_not_Delete_Student()
        {
            Assert.Equal(-1, _studentService.Delete(-13445));
        }

        [Fact]
        public void should_Delete_Student()
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                Assert.Equal(1, _studentService.Delete(1));
            }
        }
    }
}
