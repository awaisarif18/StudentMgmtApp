using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Departments
        public ActionResult Index()
        {
            var list = db.Departments
                         .Select(d => new DepartmentViewModel
                         {
                             DepartmentId = d.DepartmentId,
                             Name = d.Name
                         })
                         .ToList();
            return View(list);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var d = db.Departments.Find(id);
            if (d == null) return HttpNotFound();
            var vm = new DepartmentViewModel { DepartmentId = d.DepartmentId, Name = d.Name };
            return View(vm);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View(new DepartmentFormViewModel());
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(new Department { Name = vm.Name });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var d = db.Departments.Find(id);
            if (d == null) return HttpNotFound();
            var vm = new DepartmentFormViewModel { DepartmentId = d.DepartmentId, Name = d.Name };
            return View(vm);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var d = db.Departments.Find(vm.DepartmentId);
                d.Name = vm.Name;
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var d = db.Departments.Find(id);
            if (d == null) return HttpNotFound();
            var vm = new DepartmentViewModel { DepartmentId = d.DepartmentId, Name = d.Name };
            return View(vm);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var d = db.Departments.Find(id);
            db.Departments.Remove(d);
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