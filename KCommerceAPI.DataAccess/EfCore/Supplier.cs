using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Supplier
    {
        public Supplier()
        {
            Addresses = new HashSet<Address>();
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public Guid Id { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierContact { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
