// Models/DashboardViewModel.cs
using System.Collections.Generic;

namespace StudentMgmtApp.Models
{
    public class DashboardViewModel
    {
        // Labels and counts for Chart 1
        public List<string> DepartmentNames { get; set; }
        public List<int> StudentsPerDepartment { get; set; }

        // Labels and counts for Chart 2
        public List<string> CourseTitles { get; set; }
        public List<int> EnrollmentsPerCourse { get; set; }
    }
}