using System.Linq;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class HomeController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        public ActionResult Index()
        {
            // Students per Department
            var deptStats = db.Departments
                              .Select(d => new {
                                  Name = d.Name,
                                  Count = d.Students.Count()
                              }).ToList();

            // Enrollments per Course
            var courseStats = db.Courses
                                .Select(c => new {
                                    Title = c.Title,
                                    Count = c.Enrollments.Count()
                                }).ToList();

            var vm = new DashboardViewModel
            {
                DepartmentNames = deptStats.Select(x => x.Name).ToList(),
                StudentsPerDepartment = deptStats.Select(x => x.Count).ToList(),
                CourseTitles = courseStats.Select(x => x.Title).ToList(),
                EnrollmentsPerCourse = courseStats.Select(x => x.Count).ToList()
            };

            return View(vm);
        }


        // GET: Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Project details and overview.";
            return View();
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact details.";
            return View();
        }
    }
}