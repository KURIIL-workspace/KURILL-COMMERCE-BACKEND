using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Employee
    {
        public Employee()
        {
            Addresses = new HashSet<Address>();
            EmployeeLogins = new HashSet<EmployeeLogin>();
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Contact { get; set; }
        public short? StatusId { get; set; }

        public virtual EmployeeStatus? Status { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}
