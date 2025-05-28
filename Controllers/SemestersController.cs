using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentMgmtApp.Models;

namespace StudentMgmtApp.Controllers
{
    public class SemestersController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Semesters
        public ActionResult Index()
        {
            var list = db.Semesters
                         .Select(s => new SemesterViewModel
                         {
                             SemesterId = s.SemesterId,
                             Name = s.Name,
                             StartDate = s.StartDate,
                             EndDate = s.EndDate
                         })
                         .ToList();
            return View(list);
        }

        // GET: Semesters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var s = db.Semesters.Find(id);
            if (s == null) return HttpNotFound();
            var vm = new SemesterViewModel { SemesterId = s.SemesterId, Name = s.Name, StartDate = s.StartDate, EndDate = s.EndDate };
            return View(vm);
        }

        // GET: Semesters/Create
        public ActionResult Create()
        {
            return View(new SemesterFormViewModel());
        }

        // POST: Semesters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SemesterFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                db.Semesters.Add(new Semester { Name = vm.Name, StartDate = vm.StartDate, EndDate = vm.EndDate });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Semesters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var s = db.Semesters.Find(id);
            if (s == null) return HttpNotFound();
            var vm = new SemesterFormViewModel { SemesterId = s.SemesterId, Name = s.Name, StartDate = s.StartDate, EndDate = s.EndDate };
            return View(vm);
        }

        // POST: Semesters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SemesterFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var s = db.Semesters.Find(vm.SemesterId);
                s.Name = vm.Name;
                s.StartDate = vm.StartDate;
                s.EndDate = vm.EndDate;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Semesters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var s = db.Semesters.Find(id);
            if (s == null) return HttpNotFound();
            var vm = new SemesterViewModel { SemesterId = s.SemesterId, Name = s.Name, StartDate = s.StartDate, EndDate = s.EndDate };
            return View(vm);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var s = db.Semesters.Find(id);
            db.Semesters.Remove(s);
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