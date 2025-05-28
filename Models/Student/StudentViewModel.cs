using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMgmtApp.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
    }
}