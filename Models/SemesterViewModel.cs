using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentMgmtApp.Models
{
    public class SemesterViewModel
    {
        public int SemesterId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}