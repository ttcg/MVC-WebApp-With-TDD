using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_WebApp_With_TDD.Models;

namespace MVC_WebApp_With_TDD.ViewModels
{
    public class StudentIndexViewModel
    {
        public Student2 Student { get; set; }
        public string CampusName { get; set; }
    }
}