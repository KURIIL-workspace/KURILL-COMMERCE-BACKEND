using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Purchase.PurchaseOrder
{
    public class PurchaseOrderInputJson
    {
        [JsonIgnore]
        public decimal? TotalPrice { 
            get
            {
                return this.Items.Sum(c => c.Qty * c.UnitPrice);
            }
        }

        [JsonPropertyName("items")]
        public virtual ICollection<PurchaseOrderItemInputJson> Items { get; set; }

    }
}
