using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            GoodsRecieveNotes = new HashSet<GoodsRecieveNote>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }

        public Guid Id { get; set; }
        public short? StatusId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        public virtual PurchaseOrderStatus? Status { get; set; }
        public virtual ICollection<GoodsRecieveNote> GoodsRecieveNotes { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
