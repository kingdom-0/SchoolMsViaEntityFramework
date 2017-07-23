using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolMsViaEntityFramework.DAL;
using SchoolMsViaEntityFramework.Models;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace SchoolMsViaEntityFramework.Controllers
{
    public class DepartmentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Administrator);
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
            return View();
        }

        // POST: Departments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID,RowVersion")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", department.InstructorID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments
                .Include(x=>x.Administrator).AsNoTracking()
                .SingleOrDefaultAsync(x=>x.DepartmentID == id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", department.InstructorID);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var departmentToUpdate = await db.Departments
                .Include(x => x.Administrator)
                .SingleOrDefaultAsync(x => x.DepartmentID == id);
            if(departmentToUpdate == null)
            {
                var deletedDepartment = new Department();
                TryUpdateModel(deletedDepartment);
                ModelState.AddModelError(string.Empty, "Unable to save changes. The department was deleted by another user.");
                ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", deletedDepartment.InstructorID);
                return View(deletedDepartment);
            }

            db.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if(TryUpdateModel(departmentToUpdate,"",new string[] { "Name","StartDate","Budget","InstructorID"}))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if(databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes.The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();
                        if(databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value:{databaseValues.Name}");
                        }
                        if(databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", $"Current value:{databaseValues.Budget:c}");
                        }
                        if(databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("StartDate", $"Current value:{databaseValues.StartDate:d}");
                        }
                        if(databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseInstructor = await db.Instructors.SingleOrDefaultAsync(x => x.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("InstructorID", $"Current value:{databaseInstructor.FullName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                        + "was modified by another user after you got the original value. The "
                        + "edit operation was canceled and the current values in the database "
                        + "have been displayed. If you still want to edit this record, click "
                        + "the Save button again. Otherwise click the Back to List hyperlink.");
                        departmentToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }

            ViewData["InstructorID"] = new SelectList(db.Instructors, "ID", "FullName", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
