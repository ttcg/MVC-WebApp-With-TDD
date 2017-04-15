using MVC_WebApp_With_TDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApp_With_TDD.ViewModels
{
    public class StudentViewModel 
    {
        public Student Student { get; set; }
        public IEnumerable<SelectListItem> Campuses { get; set; }
    }
}