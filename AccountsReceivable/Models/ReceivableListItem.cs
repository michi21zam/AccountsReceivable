using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountsReceivable.Models
{
    public class ReceivableListItem
    {
        public int ReceivableId { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Balance { get; set; }
        public ReceivableStatus Status { get; set; }
    }

}