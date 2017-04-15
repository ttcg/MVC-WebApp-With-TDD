using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebApp_With_TDD.Models
{
    public class Campus
    {
        [Key]
        public int CampusID { get; set; }
        public string CampusName { get; set; }
    }
}