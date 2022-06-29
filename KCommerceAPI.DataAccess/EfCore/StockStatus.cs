using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class StockStatus
    {
        public StockStatus()
        {
            Stocks = new HashSet<Stock>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
