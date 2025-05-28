using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class CoursesController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Courses
        public ActionResult Index()
        {
            var list = db.Courses
                         .Include(c => c.Department)
                         .Select(c => new CourseViewModel
                         {
                             CourseId = c.CourseId,
                             Title = c.Title,
                             Credits = c.Credits,
                             DepartmentName = c.Department.Name
                         })
                         .ToList();
            return View(list);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var c = db.Courses
                      .Include(cu => cu.Department)
                      .FirstOrDefault(cu => cu.CourseId == id);
            if (c == null) return HttpNotFound();

            var vm = new CourseViewModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentName = c.Department.Name
            };
            return View(vm);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            var form = new CourseFormViewModel
            {
                Departments = db.Departments
                                .Select(d => new SelectListItem
                                {
                                    Value = d.DepartmentId.ToString(),
                                    Text = d.Name
                                })
                                .ToList()
            };
            return View(form);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Title = form.Title,
                    Credits = form.Credits,
                    DepartmentId = form.DepartmentId
                };
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // repopulate on error
            form.Departments = db.Departments
                                 .Select(d => new SelectListItem
                                 {
                                     Value = d.DepartmentId.ToString(),
                                     Text = d.Name
                                 })
                                 .ToList();
            return View(form);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var c = db.Courses.Find(id);
            if (c == null) return HttpNotFound();

            var form = new CourseFormViewModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentId = c.DepartmentId,
                Departments = db.Departments
                                 .Select(d => new SelectListItem
                                 {
                                     Value = d.DepartmentId.ToString(),
                                     Text = d.Name
                                 })
                                 .ToList()
            };
            return View(form);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var c = db.Courses.Find(form.CourseId);
                c.Title = form.Title;
                c.Credits = form.Credits;
                c.DepartmentId = form.DepartmentId;
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // repopulate on error
            form.Departments = db.Departments
                                 .Select(d => new SelectListItem
                                 {
                                     Value = d.DepartmentId.ToString(),
                                     Text = d.Name
                                 })
                                 .ToList();
            return View(form);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var c = db.Courses
                      .Include(cu => cu.Department)
                      .FirstOrDefault(cu => cu.CourseId == id);
            if (c == null) return HttpNotFound();

            var vm = new CourseViewModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentName = c.Department.Name
            };
            return View(vm);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var c = db.Courses.Find(id);
            db.Courses.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
