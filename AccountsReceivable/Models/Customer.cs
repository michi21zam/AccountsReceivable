using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.Models
{
    // Customer who owes money to the company
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required, StringLength(20)]
        public string CustomerCode { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(30)]
        [Display(Name = "Tax Id")]
        public string TaxId { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Column(TypeName = "decimal")]
        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }

        public Customer()
        {
            Receivables = new HashSet<Receivable>();
        }
    }
}
