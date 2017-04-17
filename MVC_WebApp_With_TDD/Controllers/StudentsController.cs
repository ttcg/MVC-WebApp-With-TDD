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
using MVC_WebApp_With_TDD.ViewModels;
using MVC_WebApp_With_TDD.Filters;

namespace MVC_WebApp_With_TDD.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentsService _studentService;
        private ICampusService _campusService;

        public StudentsController(
            IStudentsService studentService,
            ICampusService campusService)
        {
            _studentService = studentService;
            _campusService = campusService;
        }

        // GET: Students
        [TestActionFilter]
        [OutputCache(Duration = 3, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult Index()
        {
            ViewBag.Test = DateTime.Now.ToString();
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
            var model = new StudentViewModel();

            model.Student = new Student();
            model.Student.RefNo = "11112222";
            model.Student.FirstName = "Test";
            model.Student.LastName = "Surname";
            model.Student.DateOfBirth = DateTime.Now;
            model.Campuses = PopulateCampusesList();

            return View(model);
        }

        private IEnumerable<SelectListItem> PopulateCampusesList()
        {
            return _campusService.GetAll().Select(x => new SelectListItem
            {
                Value = x.CampusID.ToString(),
                Text = x.CampusName
            });
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.Insert(model.Student);
                return RedirectToAction("Index");
            }

            model.Campuses = PopulateCampusesList();

            return View(model);
        }

        // GET: Students/Edit/5        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new StudentViewModel();
            model.Student = _studentService.GetDetail(id.Value); 

            if (model.Student == null)
            {
                return HttpNotFound();
            }
            
            model.Campuses = PopulateCampusesList();

            return View(model);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.Update(model.Student);
                return RedirectToAction("Index");
            }

            model.Campuses = PopulateCampusesList();
            return View(model);
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
