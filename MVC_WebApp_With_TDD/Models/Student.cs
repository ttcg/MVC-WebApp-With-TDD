using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WebApp_With_TDD.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string RefNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateModified { get; set; }

    }
}