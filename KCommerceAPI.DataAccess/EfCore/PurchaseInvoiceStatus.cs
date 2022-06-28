using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseInvoiceStatus
    {
        public PurchaseInvoiceStatus()
        {
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}
