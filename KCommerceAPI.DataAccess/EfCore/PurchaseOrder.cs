using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            GoodsRecieveNotes = new HashSet<GoodsRecieveNote>();
            PurchaseInvoices = new HashSet<PurchaseInvoice>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }

        public Guid Id { get; set; }
        public short? StatusId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public DateTime? OrderDate { get; set; }
        public Guid? SupplierId { get; set; }
        public int? TotalQty { get; set; }
        public string? TermsCondition { get; set; }
        public string? ShipToAddress { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? PreparedEmployee { get; set; }

        public virtual Employee? PreparedEmployeeNavigation { get; set; }
        public virtual PurchaseOrderStatus? Status { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<GoodsRecieveNote> GoodsRecieveNotes { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
