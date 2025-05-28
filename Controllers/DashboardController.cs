using System.Linq;
using System.Web.Mvc;
using StudentMgmtApp.Models;

public class DashboardController : Controller
{
    private StudentDbEntities db = new StudentDbEntities();

    public ActionResult Index()
    {
        // Example: Enrollment counts per course
        var data = db.Enrollments
            .GroupBy(e => e.Course.Title)
            .Select(g => new { Course = g.Key, Count = g.Count() })
            .ToList();
        ViewBag.EnrollData = data;
        return View();
    }
}