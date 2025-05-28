using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMgmtApp.Models
{
    public class EnrollmentFormViewModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public string Grade { get; set; }

        public IEnumerable<SelectListItem> Students { get; set; }  // Dropdown
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<SelectListItem> Semesters { get; set; }
    }
}