using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AccountsReceivable.Models
{
    // An account receivable: money a customer owes to the company
    public class Receivable
    {
        public int ReceivableId { get; set; }

        [Required, StringLength(30)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public ReceivableStatus Status { get; set; }

        public bool IsActive { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public Receivable()
        {
            Payments = new HashSet<Payment>();
            Status = ReceivableStatus.Pending;
        }

        // Amount already paid, based on active payments
        [NotMapped]
        public decimal PaidAmount
        {
            get { return Payments == null ? 0 : Payments.Where(p => p.IsActive).Sum(p => p.Amount); }
        }

        // Amount still owed
        [NotMapped]
        public decimal Balance
        {
            get { return TotalAmount - PaidAmount; }
        }

        // Recalculates the Status field based on current payments
        public void RecalculateStatus()
        {
            var paid = PaidAmount;

            if (paid <= 0)
                Status = ReceivableStatus.Pending;
            else if (paid < TotalAmount)
                Status = ReceivableStatus.PartiallyPaid;
            else
                Status = ReceivableStatus.Paid;
        }
    }
}
