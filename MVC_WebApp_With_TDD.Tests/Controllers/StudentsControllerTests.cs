using MVC_WebApp_With_TDD.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MVC_WebApp_With_TDD.Services;
using System.Web.Mvc;
using MVC_WebApp_With_TDD.Models;
using System.Net;

namespace MVC_WebApp_With_TDD.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private StudentsController _controller;
        Mock<IStudentsService> _studentService;

        public StudentsControllerTests()
        {
            _studentService = new Mock<IStudentsService>();
            _controller = new StudentsController(_studentService.Object);
        }

        [Fact]
        public void Index()
        {            
            var studentList = new List<Student>()
            {
            new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now },
            new Student() { StudentID = 2, RefNo = "87984564", FirstName = "Pete", LastName = "Luck", DateOfBirth = DateTime.Now.AddYears(-20), DateCreated = DateTime.Now.AddDays(1) }
            };

            _studentService.Setup(s => s.GetAll()).Returns(studentList);            

            ViewResult vr = _controller.Index() as ViewResult;

            Assert.NotNull(vr);
            var model = vr.Model as List<Student>;
            Assert.True(model.Count == 2);

            _studentService.Verify();
        }        

        [Fact]
        public void Details()
        {
            var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _studentService.Setup(s => s.GetDetail(1)).Returns(student);

            ViewResult vr = _controller.Details(1, "test") as ViewResult;

            Assert.NotNull(vr);

            var model = vr.Model as Student;

            Assert.Equal(1, model.StudentID);
            Assert.Equal("Johntest", model.FirstName);

            _studentService.Verify();
        }

        [Fact]
        public void should_return_notfound_Details()
        {
            _studentService.Setup(s => s.GetDetail(1)).Returns<Student>(null);

            var vr = _controller.Details(1, "test") as HttpNotFoundResult;

            Assert.NotNull(vr);

            _studentService.Verify();
        }

        [Fact]
        public void Create()
        {
            var vr = _controller.Create() as ViewResult;

            Assert.NotNull(vr);
        }

        [Fact]
        public void should_redirect_CreatePost()
        {
            var student = new Student() { StudentID = 1, RefNo = "12456343", FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _studentService.Setup(s => s.Insert(student)).Returns(1);

            var vr = _controller.Create(student) as RedirectToRouteResult;            

            Assert.NotNull(vr);

            Assert.Equal("Index", vr.RouteValues["action"]);

            _studentService.Verify();
        }

        [Fact]
        public void should_fail_CreatePost()
        {
            var student = new Student() { StudentID = 1, RefNo = string.Empty, FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _controller.ModelState.AddModelError("RefNo", "Invalid RefNo");

            var vr = _controller.Create(student) as ViewResult;

            Assert.NotNull(vr);

            var model = vr.Model as Student;

            Assert.Equal(student.StudentID, model.StudentID);
            Assert.Equal(student.FirstName, model.FirstName);
        }

        [Fact]
        public void Edit()
        {
            var student = new Student() { StudentID = 1, RefNo = string.Empty, FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _studentService.Setup(s => s.GetDetail(1)).Returns(student);

            var vr = _controller.Edit(1) as ViewResult;

            Assert.NotNull(vr);

            var model = vr.Model as Student;

            Assert.Equal(student.StudentID, model.StudentID);
            Assert.Equal(student.FirstName, model.FirstName);

            _studentService.Verify();
        }

        [Fact]
        public void should_redirect_EditPost()
        {
            var student = new Student() { StudentID = 1, RefNo = string.Empty, FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _studentService.Setup(s => s.Update(student)).Returns(1);

            var vr = _controller.Edit(student) as RedirectToRouteResult;

            Assert.NotNull(vr);
            Assert.Equal("Index", vr.RouteValues["action"]);            
        }

        public void should_fail_EditPost()
        {
            var student = new Student() { StudentID = 1, RefNo = string.Empty, FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };
            _controller.ModelState.AddModelError("RefNo", "Invalid RefNo");

            var vr = _controller.Edit(student) as ViewResult;
            var model = vr.Model as Student;

            Assert.NotNull(vr);
            Assert.Equal(student.StudentID, model.StudentID);
        }

        [Fact]
        public void Delete()
        {
            var student = new Student() { StudentID = 1, RefNo = string.Empty, FirstName = "John", LastName = "Smith", DateOfBirth = DateTime.Now.AddYears(-10), DateCreated = DateTime.Now };

            _studentService.Setup(s => s.GetDetail(1)).Returns(student);

            var vr = _controller.Delete(1) as ViewResult;
            var model = vr.Model as Student;

            Assert.NotNull(vr);
            Assert.Equal(student.StudentID,  model.StudentID);

            _studentService.Verify();
        }

        [Fact]
        public void should_return_badResult_Delete()
        {
            var vr = _controller.Delete(null) as HttpStatusCodeResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, vr.StatusCode);
        }

        [Fact]
        public void Delete_Post()
        {
            _studentService.Setup(s => s.Delete(1)).Returns(1);

            var vr = _controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.Equal("Index", vr.RouteValues["action"]);

            _studentService.Verify();
        }
    }
}
