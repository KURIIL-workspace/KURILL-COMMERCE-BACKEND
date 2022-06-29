using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }

        public Guid Id { get; set; }
        public Guid? CusId { get; set; }
        public string? CusName { get; set; }
        public string? CategoryName { get; set; }
        public string? ItemName { get; set; }
        public int? OdrQty { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public short? StatusId { get; set; }
        public string? Status { get; set; }
        public Guid? StockId { get; set; }

        public virtual Customer? Cus { get; set; }
        public virtual Stock? Stock { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
