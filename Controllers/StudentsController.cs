using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Students
        public ActionResult Index()
        {
            var list = db.Students
                         .Include(s => s.Department)
                         .Select(s => new StudentViewModel
                         {
                             StudentId = s.StudentId,
                             FirstName = s.FirstName,
                             LastName = s.LastName,
                             Email = s.Email,
                             DepartmentName = s.Department.Name
                         })
                         .ToList();
            return View(list);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var s = db.Students
                      .Include(x => x.Department)
                      .FirstOrDefault(x => x.StudentId == id);
            if (s == null)
                return HttpNotFound();

            var vm = new StudentViewModel
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DepartmentName = s.Department.Name
            };
            return View(vm);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            var vm = new StudentFormViewModel
            {
                Departments = db.Departments
                                 .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
                                 .ToList()
            };
            return View(vm);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    DOB = vm.DOB,
                    Email = vm.Email,
                    DepartmentId = vm.DepartmentId
                };
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Departments = db.Departments
                                 .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
                                 .ToList();
            return View(vm);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var s = db.Students.Find(id);
            if (s == null) return HttpNotFound();

            var vm = new StudentFormViewModel
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DOB = s.DOB,
                Email = s.Email,
                DepartmentId = s.DepartmentId,
                Departments = db.Departments
                                 .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
                                 .ToList()
            };
            return View(vm);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = db.Students.Find(vm.StudentId);
                student.FirstName = vm.FirstName;
                student.LastName = vm.LastName;
                student.DOB = vm.DOB;
                student.Email = vm.Email;
                student.DepartmentId = vm.DepartmentId;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Departments = db.Departments
                                 .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
                                 .ToList();
            return View(vm);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var s = db.Students
                      .Include(x => x.Department)
                      .FirstOrDefault(x => x.StudentId == id);
            if (s == null) return HttpNotFound();

            var vm = new StudentViewModel
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                DepartmentName = s.Department.Name
            };
            return View(vm);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.Students.Find(id);
            db.Students.Remove(student);
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