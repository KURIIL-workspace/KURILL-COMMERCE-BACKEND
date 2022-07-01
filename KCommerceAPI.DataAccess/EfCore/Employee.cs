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
            GoodsRecieveNotes = new HashSet<GoodsRecieveNote>();
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Stocks = new HashSet<Stock>();
        }

        public string Code { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid Id { get; set; }
        public string? Contact { get; set; }
        public short? StatusId { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual EmployeeStatus? Status { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }
        public virtual ICollection<GoodsRecieveNote> GoodsRecieveNotes { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
