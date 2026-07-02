using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AccountsReceivable.Models;

namespace AccountsReceivable.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AccountsReceivableContext db = new AccountsReceivableContext();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customers = await db.Customers.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync();
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = await db.Customers.FindAsync(id.Value);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View(new Customer { IsActive = true });
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "CustomerCode,Name,TaxId,Email,Phone,Address,CreditLimit")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.IsActive = true;
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = await db.Customers.FindAsync(id.Value);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "CustomerId,CustomerCode,Name,TaxId,Email,Phone,Address,CreditLimit")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var dbCustomer = await db.Customers.FindAsync(customer.CustomerId);
                if (dbCustomer == null)
                    return HttpNotFound();

                dbCustomer.CustomerCode = customer.CustomerCode;
                dbCustomer.Name = customer.Name;
                dbCustomer.TaxId = customer.TaxId;
                dbCustomer.Email = customer.Email;
                dbCustomer.Phone = customer.Phone;
                dbCustomer.Address = customer.Address;
                dbCustomer.CreditLimit = customer.CreditLimit;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = await db.Customers.FindAsync(id.Value);
            if (customer == null || !customer.IsActive)
                return HttpNotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5 (Soft Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var customer = await db.Customers.FindAsync(id);

            if (customer != null)
            {
                customer.IsActive = false;
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
