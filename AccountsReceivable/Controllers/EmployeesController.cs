using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AccountsReceivable.Models;

namespace AccountsReceivable.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AccountsReceivableContext db = new AccountsReceivableContext();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = await db.Employees.Where(e => e.IsActive).OrderBy(e => e.FirstName).ToListAsync();
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = await db.Employees.FindAsync(id.Value);
            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new Employee { HireDate = System.DateTime.Now, IsActive = true });
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "EmployeeCode,FirstName,LastName,Email,Phone,Position,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.IsActive = true;
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = await db.Employees.FindAsync(id.Value);
            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "EmployeeId,EmployeeCode,FirstName,LastName,Email,Phone,Position,HireDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var dbEmployee = await db.Employees.FindAsync(employee.EmployeeId);
                if (dbEmployee == null)
                    return HttpNotFound();

                dbEmployee.EmployeeCode = employee.EmployeeCode;
                dbEmployee.FirstName = employee.FirstName;
                dbEmployee.LastName = employee.LastName;
                dbEmployee.Email = employee.Email;
                dbEmployee.Phone = employee.Phone;
                dbEmployee.Position = employee.Position;
                dbEmployee.HireDate = employee.HireDate;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = await db.Employees.FindAsync(id.Value);
            if (employee == null || !employee.IsActive)
                return HttpNotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5 (Soft Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var employee = await db.Employees.FindAsync(id);

            if (employee != null)
            {
                employee.IsActive = false;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
