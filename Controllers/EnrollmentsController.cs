using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class EnrollmentsController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Enrollments
        public ActionResult Index()
        {
            var list = db.Enrollments
                         .Include(e => e.Student)
                         .Include(e => e.Course)
                         .Include(e => e.Semester)
                         .Select(e => new EnrollmentViewModel
                         {
                             EnrollmentId = e.EnrollmentId,
                             StudentName = e.Student.FirstName + " " + e.Student.LastName,
                             CourseTitle = e.Course.Title,
                             SemesterName = e.Semester.Name,
                             Grade = e.Grade
                         })
                         .ToList();
            return View(list);
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var e = db.Enrollments
                      .Include(x => x.Student)
                      .Include(x => x.Course)
                      .Include(x => x.Semester)
                      .FirstOrDefault(x => x.EnrollmentId == id);
            if (e == null) return HttpNotFound();
            var vm = new EnrollmentViewModel
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FirstName + " " + e.Student.LastName,
                CourseTitle = e.Course.Title,
                SemesterName = e.Semester.Name,
                Grade = e.Grade
            };
            return View(vm);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            var vm = new EnrollmentFormViewModel
            {
                Students = db.Students.Select(s => new SelectListItem { Value = s.StudentId.ToString(), Text = s.FirstName + " " + s.LastName }),
                Courses = db.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.Title }),
                Semesters = db.Semesters.Select(s => new SelectListItem { Value = s.SemesterId.ToString(), Text = s.Name })
            };
            return View(vm);
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnrollmentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(new Enrollment
                {
                    StudentId = vm.StudentId,
                    CourseId = vm.CourseId,
                    SemesterId = vm.SemesterId,
                    Grade = vm.Grade
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Students = db.Students.Select(s => new SelectListItem { Value = s.StudentId.ToString(), Text = s.FirstName + " " + s.LastName });
            vm.Courses = db.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.Title });
            vm.Semesters = db.Semesters.Select(s => new SelectListItem { Value = s.SemesterId.ToString(), Text = s.Name });
            return View(vm);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var e = db.Enrollments.Find(id);
            if (e == null) return HttpNotFound();
            var vm = new EnrollmentFormViewModel
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                SemesterId = e.SemesterId,
                Grade = e.Grade,
                Students = db.Students.Select(s => new SelectListItem { Value = s.StudentId.ToString(), Text = s.FirstName + " " + s.LastName }),
                Courses = db.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.Title }),
                Semesters = db.Semesters.Select(s => new SelectListItem { Value = s.SemesterId.ToString(), Text = s.Name })
            };
            return View(vm);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnrollmentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var e = db.Enrollments.Find(vm.EnrollmentId);
                e.StudentId = vm.StudentId;
                e.CourseId = vm.CourseId;
                e.SemesterId = vm.SemesterId;
                e.Grade = vm.Grade;
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Students = db.Students.Select(s => new SelectListItem { Value = s.StudentId.ToString(), Text = s.FirstName + " " + s.LastName });
            vm.Courses = db.Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.Title });
            vm.Semesters = db.Semesters.Select(s => new SelectListItem { Value = s.SemesterId.ToString(), Text = s.Name });
            return View(vm);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var e = db.Enrollments
                      .Include(x => x.Student)
                      .Include(x => x.Course)
                      .Include(x => x.Semester)
                      .FirstOrDefault(x => x.EnrollmentId == id);
            if (e == null) return HttpNotFound();
            var vm = new EnrollmentViewModel
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FirstName + " " + e.Student.LastName,
                CourseTitle = e.Course.Title,
                SemesterName = e.Semester.Name,
                Grade = e.Grade
            };
            return View(vm);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var e = db.Enrollments.Find(id);
            db.Enrollments.Remove(e);
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