using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseInvoiceItem
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ItemName { get; set; }
        public short? ItemQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual PurchaseOrderItem? PurchaseInvoice { get; set; }
    }
}
