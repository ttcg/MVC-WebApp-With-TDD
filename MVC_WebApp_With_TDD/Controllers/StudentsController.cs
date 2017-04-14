using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MVC_WebApp_With_TDD.DbContexts;
using MVC_WebApp_With_TDD.Models;
using MVC_WebApp_With_TDD.Services;

namespace MVC_WebApp_With_TDD.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentsService _studentService; 

        public StudentsController(IStudentsService studentService)
        {
            _studentService = studentService;
        }

        // GET: Students
        public ActionResult Index()
        {
            return View(_studentService.GetAll().ToList());
        }

        // GET: Students/Details/5
        [Route("Students/Details/{id}/{name?}")]
        public ActionResult Details(int? id, string name)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.GetDetail(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            student.FirstName += name;
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,RefNo,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Insert(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.GetDetail(id.Value); 
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,RefNo,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentService.GetDetail(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("Students/TestRoute/{FirstName}/{Surname}")]
        public ActionResult TestRoute(string FirstName, string Surname)
        {
            return Json(_studentService.GetByName(FirstName, Surname), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
