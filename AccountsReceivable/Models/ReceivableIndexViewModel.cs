using System;
using System.Collections.Generic;

namespace AccountsReceivable.Models
{
    public class ReceivableIndexViewModel
    {
        // Ahora apunta al modelo plano ReceivableListItem
        public IEnumerable<ReceivableListItem> Receivables { get; set; }

        // Filters
        public string AccountNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public ReceivableStatus? Status { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }

        // Dropdown sources
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        // Paging
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
