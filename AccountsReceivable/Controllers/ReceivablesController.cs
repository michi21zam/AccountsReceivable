using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AccountsReceivable.Models;

namespace AccountsReceivable.Controllers
{
    public class ReceivablesController : Controller
    {
        private readonly AccountsReceivableContext db = new AccountsReceivableContext();

        // GET: Receivables
        public async Task<ActionResult> Index(
            string accountNumber,
            int? customerId,
            int? employeeId,
            ReceivableStatus? status,
            DateTime? dateFrom,
            DateTime? dateTo,
            decimal? amountFrom,
            decimal? amountTo,
            int page = 1,
            int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = db.Receivables
                .Include(r => r.Customer)
                .Include(r => r.Employee)
                .Where(r => r.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(accountNumber))
                query = query.Where(r => r.AccountNumber.Contains(accountNumber));

            if (customerId.HasValue)
                query = query.Where(r => r.CustomerId == customerId.Value);

            if (employeeId.HasValue)
                query = query.Where(r => r.EmployeeId == employeeId.Value);

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            if (dateFrom.HasValue)
                query = query.Where(r => DbFunctions.TruncateTime(r.IssueDate) >= DbFunctions.TruncateTime(dateFrom.Value));

            if (dateTo.HasValue)
                query = query.Where(r => DbFunctions.TruncateTime(r.IssueDate) <= DbFunctions.TruncateTime(dateTo.Value));

            if (amountFrom.HasValue)
                query = query.Where(r => r.TotalAmount >= amountFrom.Value);

            if (amountTo.HasValue)
                query = query.Where(r => r.TotalAmount <= amountTo.Value);

            query = query.OrderByDescending(r => r.IssueDate);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var receivables = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var vm = new ReceivableIndexViewModel
            {
                Receivables = receivables,
                AccountNumber = accountNumber,
                CustomerId = customerId,
                EmployeeId = employeeId,
                Status = status,
                DateFrom = dateFrom,
                DateTo = dateTo,
                AmountFrom = amountFrom,
                AmountTo = amountTo,
                Customers = await db.Customers.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync(),
                Employees = await db.Employees.Where(e => e.IsActive).OrderBy(e => e.FirstName).ToListAsync(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return View(vm);
        }

        // GET: Receivables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receivable = await db.Receivables
                .Include(r => r.Customer)
                .Include(r => r.Employee)
                .Include("Payments")
                .FirstOrDefaultAsync(r => r.ReceivableId == id.Value);

            if (receivable == null)
                return HttpNotFound();

            return View(receivable);
        }

        // GET: Receivables/Create
        public async Task<ActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new Receivable { IssueDate = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IsActive = true });
        }

        // POST: Receivables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "AccountNumber,CustomerId,EmployeeId,IssueDate,DueDate,TotalAmount,Description")] Receivable receivable)
        {
            if (ModelState.IsValid)
            {
                receivable.IsActive = true;
                receivable.Status = ReceivableStatus.Pending;

                db.Receivables.Add(receivable);
                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { id = receivable.ReceivableId });
            }

            await PopulateDropdowns();
            return View(receivable);
        }

        // GET: Receivables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receivable = await db.Receivables.FindAsync(id.Value);
            if (receivable == null)
                return HttpNotFound();

            await PopulateDropdowns();
            return View(receivable);
        }

        // POST: Receivables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "ReceivableId,AccountNumber,CustomerId,EmployeeId,IssueDate,DueDate,TotalAmount,Description")] Receivable receivable)
        {
            if (ModelState.IsValid)
            {
                var dbReceivable = await db.Receivables
                    .Include("Payments")
                    .FirstOrDefaultAsync(r => r.ReceivableId == receivable.ReceivableId);

                if (dbReceivable == null)
                    return HttpNotFound();

                dbReceivable.AccountNumber = receivable.AccountNumber;
                dbReceivable.CustomerId = receivable.CustomerId;
                dbReceivable.EmployeeId = receivable.EmployeeId;
                dbReceivable.IssueDate = receivable.IssueDate;
                dbReceivable.DueDate = receivable.DueDate;
                dbReceivable.TotalAmount = receivable.TotalAmount;
                dbReceivable.Description = receivable.Description;

                // Recalculate status since TotalAmount may have changed
                dbReceivable.RecalculateStatus();

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = receivable.ReceivableId });
            }

            await PopulateDropdowns();
            return View(receivable);
        }

        // GET: Receivables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receivable = await db.Receivables
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReceivableId == id.Value);

            if (receivable == null || !receivable.IsActive)
                return HttpNotFound();

            return View(receivable);
        }

        // POST: Receivables/Delete/5 (Soft Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var receivable = await db.Receivables.FindAsync(id);

            if (receivable != null && receivable.IsActive)
            {
                receivable.IsActive = false;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.Customers = await db.Customers.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync();
            ViewBag.Employees = await db.Employees.Where(e => e.IsActive).OrderBy(e => e.FirstName).ToListAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
