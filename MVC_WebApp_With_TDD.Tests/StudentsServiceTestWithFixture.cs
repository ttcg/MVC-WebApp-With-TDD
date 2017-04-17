using Moq;
using MVC_WebApp_With_TDD.Models;
using MVC_WebApp_With_TDD.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MVC_WebApp_With_TDD.Tests
{
    public class StudentsServiceTestWithFixture : IClassFixture<StudentsServiceTestFixture>
    {
        private IStudentsService _studentService;
        private Mock<DbContexts.MVCWebAppDbContext> _dbContext;
        private List<Student> _students;

        public StudentsServiceTestWithFixture(StudentsServiceTestFixture fixture)
        {
            _dbContext = fixture.MVCWebAppDbContext;

            _studentService = fixture.StudentService;

            //_students = fixture.Students;
        }

        [Fact]
        public void should_Add_Student()
        {
            var student = new Student() { StudentID = 3, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

            _studentService.Insert(student);

            Assert.True(student.StudentID > 0);
        }

        [Fact]
        public void GetAll()
        {
            var result = _studentService.GetAll();            

            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.FirstOrDefault().StudentID);

            _dbContext.Verify(v => v.Students.Include("Campus"), Times.Once);
        }

        [Fact]
        public void GetDetail()
        {
            var result = _studentService.GetDetail(1);

            Assert.Equal(1, result.StudentID);
        }

        [Fact]
        public void Update()
        {
            var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

            _studentService.Update(student);

            Assert.True(student.StudentID > 0);

            _dbContext.VerifyAll();
        }

        //[Fact]
        //public void Delete()
        //{            
        //    int idToDelete = 2;            

        //    // call delete method now
        //    _studentService.Delete(idToDelete);

        //    _dbContext.Setup(m => m.Students.Remove(It.IsAny<Student>())).Callback<Student>((entity) => _students.Remove(entity));

        //    _dbContext.Setup(s => s.Students.Find(idToDelete)).Returns(_students.Single(s => s.StudentID == idToDelete));

        //    // 1 object deleted, it should return 1
        //    Assert.Equal(1, _students.Count());  // <----- Error here

        //    _dbContext.Verify(s => s.Students.Find(idToDelete), Times.Once);
        //    _dbContext.Verify(s => s.Students.Remove(It.IsAny<Student>()), Times.Once);
        //    _dbContext.Verify(s => s.SaveChanges(), Times.Once);
        //}

        [Fact]
        public void GetByName()
        {
            var result = _studentService.GetByName("John", "Smith");

            Assert.Equal(1, result.StudentID);
        }
    }
}
