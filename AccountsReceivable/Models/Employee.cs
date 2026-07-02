using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountsReceivable.Models
{
    // Employee in charge of managing customer accounts receivable
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        public string EmployeeCode { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }

        public Employee()
        {
            Receivables = new HashSet<Receivable>();
        }
    }
}
