using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Purchase.PurchaseInvoice
{
    public class PurchaseInvoiceItemInputJson
    {
        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }

        [JsonPropertyName("item_name")]
        public string? ItemName { get; set; }

        [JsonPropertyName("item_quantity")]
        public short? ItemQuantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal? UnitPrice { get; set; }
        
    }
}
