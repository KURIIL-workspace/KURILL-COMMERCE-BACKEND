using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Address
    {
        public Guid Id { get; set; }
        public string? AddLine1 { get; set; }
        public string? AddLine2 { get; set; }
        public int? PostalCode { get; set; }
        public string? Country { get; set; }
        public Guid? Supplier { get; set; }
        public Guid? Employee { get; set; }
        public Guid? Customer { get; set; }

        public virtual Customer? CustomerNavigation { get; set; }
        public virtual Employee? EmployeeNavigation { get; set; }
        public virtual Supplier? SupplierNavigation { get; set; }
    }
}
