using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Category
    {
        public Category()
        {
            PurchaseInvoiceItems = new HashSet<PurchaseInvoiceItem>();
        }

        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }

        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
    }
}
