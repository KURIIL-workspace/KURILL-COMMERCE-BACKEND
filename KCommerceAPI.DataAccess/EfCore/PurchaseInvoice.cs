using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseInvoice
    {
        public PurchaseInvoice()
        {
            GoodsRecieveNotes = new HashSet<GoodsRecieveNote>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public short? StatusId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? PreparedEmployee { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public int? TotalItemQty { get; set; }
        public int? TotalAmount { get; set; }

        public virtual Employee? PreparedEmployeeNavigation { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public virtual PurchaseInvoiceStatus? Status { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<GoodsRecieveNote> GoodsRecieveNotes { get; set; }
        public List<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
    }
}
