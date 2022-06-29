using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class SalesOrderItem
    {
        public short Id { get; set; }
        public string? ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? SalesOrderId { get; set; }

        public virtual SalesOrder? SalesOrder { get; set; }
    }
}
