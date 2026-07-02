using System;
using System.Collections.Generic;

namespace AccountsReceivable.Models
{
    public class ReceivableDetailsViewModel
    {
        public int ReceivableId { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}