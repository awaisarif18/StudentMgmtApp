using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMgmtApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string DepartmentName { get; set; }
    }
}