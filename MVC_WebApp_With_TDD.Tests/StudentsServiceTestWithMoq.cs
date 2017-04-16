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
    public class StudentsServiceTestWithMoq
    {
        private IStudentsService _studentService;
        private Mock<DbContexts.MVCWebAppDbContext> _dbContext;

        public StudentsServiceTestWithMoq()
        {
            _dbContext = new Mock<DbContexts.MVCWebAppDbContext>();

            _studentService = new StudentsService(_dbContext.Object);
        }

        [Fact]
        public void should_Add_Student()
        {
            var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

            var mockSet = new Mock<DbSet<Student>>();
            _dbContext.Setup(s => s.Students).Returns(mockSet.Object);

            _studentService.Insert(student);

            Assert.True(student.StudentID > 0);

            _dbContext.VerifyAll();
        }

        [Fact]
        public void GetAll()
        {
            var campus = new Campus() { CampusID = 1, CampusName = "SMF" };
            var students = new List<Student>()
            {
                new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now, CampusID = 1, Campus = campus },
                new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1), CampusID = 2, Campus = campus }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            _dbContext.Setup(c => c.Students).Returns(mockSet.Object);

            _dbContext.Setup(m => m.Students.Include("Campus")).Returns(mockSet.Object);

            var result = _studentService.GetAll();
            

            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.FirstOrDefault().StudentID);

            _dbContext.Verify(v => v.Students.Include("Campus"), Times.Once);
        }

        [Fact]
        public void GetDetail()
        {
            //var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

            //_dbContext.Setup(s => s.Students.Find(1)).Returns(student);

            var students = new List<Student>()
            {
                new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now },
                new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1) }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

            _dbContext.Setup(c => c.Students).Returns(mockSet.Object);

            var result = _studentService.GetDetail(2);

            Assert.Equal(2, result.StudentID);
        }

        [Fact]
        public void Update()
        {
            var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10) };

            _studentService.Update(student);

            Assert.True(student.StudentID > 0);

            _dbContext.VerifyAll();
        }

        [Fact]
        public void Delete()
        {            
            Mock<DbContexts.MVCWebAppDbContext> dbContext = new Mock<DbContexts.MVCWebAppDbContext>();
            IStudentsService studentService = new StudentsService(dbContext.Object);

            var students = new List<Student>()
            {
                new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now },
                new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1) }
            };

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.AsQueryable().Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.AsQueryable().GetEnumerator());
            
            dbContext.Setup(m => m.Students.Remove(It.IsAny<Student>())).Callback<Student>((entity) => students.Remove(entity));

            dbContext.Setup(c => c.Students).Returns(mockSet.Object);

            int idToDelete = 1;

            dbContext.Setup(s => s.Students.Find(idToDelete)).Returns(students.Single(s => s.StudentID == idToDelete));

            // call delete method now
            studentService.Delete(idToDelete);

            // 1 object deleted, it should return 1
            Assert.Equal(1, students.Count());  // <----- Error here

            dbContext.Verify(s => s.Students.Find(idToDelete), Times.Once);
            dbContext.Verify(s => s.Students.Remove(It.IsAny<Student>()), Times.Once);
            dbContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetByName()
        {
            var students = new List<Student>()
            {
                new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now },
                new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1) }
            };

            var mockSet = new Mock<DbSet<Student>>();
            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(students.AsQueryable().Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(students.AsQueryable().GetEnumerator());

            _dbContext.Setup(c => c.Students).Returns(mockSet.Object);

            var result = _studentService.GetByName("Pete", "Luck");
            Assert.Equal(2, result.StudentID);

            _dbContext.VerifyAll();
        }
    }
}
