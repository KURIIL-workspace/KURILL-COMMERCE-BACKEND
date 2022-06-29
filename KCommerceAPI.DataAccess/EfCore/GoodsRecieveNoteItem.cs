using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class GoodsRecieveNoteItem
    {
        public Guid Id { get; set; }
        public Guid? GoodsRecieveNoteId { get; set; }
        public int? OrderedQty { get; set; }
        public int? RecievedQty { get; set; }
        public int? DamagedQty { get; set; }
        public int? OtherDeductedQty { get; set; }
        public int? RemainingQty { get; set; }
        public string? Description { get; set; }
        public DateOnly? CreatedDateTime { get; set; }
        public DateOnly? UpdatedDateTime { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual GoodsRecieveNote? GoodsRecieveNote { get; set; }
    }
}
