using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseOrderItem
    {
        public PurchaseOrderItem()
        {
            PurchaseInvoiceItems = new HashSet<PurchaseInvoiceItem>();
        }

        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? PurchaseOrderId { get; set; }

        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
    }
}
