using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class GoodsRecieveNote
    {
        public GoodsRecieveNote()
        {
            GoodsRecieveNoteItems = new HashSet<GoodsRecieveNoteItem>();
        }

        public Guid Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? CheckedEmployeeId { get; set; }
        public DateOnly? CheckedDate { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public string? Description { get; set; }

        public virtual Employee? CheckedEmployee { get; set; }
        public virtual PurchaseInvoice? PurchaseInvoice { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public virtual ICollection<GoodsRecieveNoteItem> GoodsRecieveNoteItems { get; set; }
    }
}
