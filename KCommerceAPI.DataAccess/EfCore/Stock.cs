using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Stock
    {
        public Stock()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ItemName { get; set; }
        public int? ItemQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public Guid? EmpId { get; set; }
        public Guid? StatusId { get; set; }

        public virtual Employee? Emp { get; set; }
        public virtual StockStatus? Status { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
