using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMgmtApp.Models
{
    public class EnrollmentViewModel
    {
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public string SemesterName { get; set; }
        public string Grade { get; set; }
    }
}