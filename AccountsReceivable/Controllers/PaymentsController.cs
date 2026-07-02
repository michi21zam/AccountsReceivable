using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AccountsReceivable.Models;

namespace AccountsReceivable.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AccountsReceivableContext db = new AccountsReceivableContext();

        // GET: Payments/Create?receivableId=5
        public async Task<ActionResult> Create(int? receivableId)
        {
            if (!receivableId.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receivable = await db.Receivables.FindAsync(receivableId.Value);
            if (receivable == null)
                return HttpNotFound();

            return View(new Payment
            {
                ReceivableId = receivableId.Value,
                PaymentDate = System.DateTime.Now,
                IsActive = true
            });
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ReceivableId,PaymentDate,Amount,Notes")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.IsActive = true;
                db.Payments.Add(payment);
                await db.SaveChangesAsync();

                await RecalculateReceivableStatus(payment.ReceivableId);

                return RedirectToAction("Details", "Receivables", new { id = payment.ReceivableId });
            }

            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var payment = await db.Payments.FindAsync(id.Value);
            if (payment == null || !payment.IsActive)
                return HttpNotFound();

            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "PaymentId,ReceivableId,PaymentDate,Amount,Notes")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var dbPayment = await db.Payments.FindAsync(payment.PaymentId);
                if (dbPayment == null)
                    return HttpNotFound();

                dbPayment.PaymentDate = payment.PaymentDate;
                dbPayment.Amount = payment.Amount;
                dbPayment.Notes = payment.Notes;

                await db.SaveChangesAsync();

                await RecalculateReceivableStatus(payment.ReceivableId);

                return RedirectToAction("Details", "Receivables", new { id = payment.ReceivableId });
            }

            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var payment = await db.Payments.FindAsync(id.Value);
            if (payment == null || !payment.IsActive)
                return HttpNotFound();

            return View(payment);
        }

        // POST: Payments/Delete/5 (Soft Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var payment = await db.Payments.FindAsync(id);

            if (payment != null && payment.IsActive)
            {
                payment.IsActive = false;
                await db.SaveChangesAsync();
                await RecalculateReceivableStatus(payment.ReceivableId);
            }

            return RedirectToAction("Details", "Receivables", new { id = payment?.ReceivableId });
        }

        // Reloads the receivable with its active payments and updates its Status
        private async Task RecalculateReceivableStatus(int receivableId)
        {
            var receivable = await db.Receivables
                .Include("Payments")
                .FirstOrDefaultAsync(r => r.ReceivableId == receivableId);

            if (receivable != null)
            {
                receivable.RecalculateStatus();
                await db.SaveChangesAsync();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
