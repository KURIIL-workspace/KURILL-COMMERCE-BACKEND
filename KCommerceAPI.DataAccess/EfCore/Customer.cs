using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public Guid Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContact { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
