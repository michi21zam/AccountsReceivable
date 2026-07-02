using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.Models
{
    // A payment (installment) applied to a Receivable
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        [Display(Name = "Receivable")]
        public int ReceivableId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }

        [StringLength(250)]
        public string Notes { get; set; }

        public bool IsActive { get; set; }

        public virtual Receivable Receivable { get; set; }

        public Payment()
        {
            IsActive = true;
            PaymentDate = DateTime.Now;
        }
    }
}
