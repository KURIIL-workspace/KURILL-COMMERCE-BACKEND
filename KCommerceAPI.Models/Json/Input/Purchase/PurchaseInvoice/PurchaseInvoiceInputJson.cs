using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Purchase.PurchaseInvoice
{
    public class PurchaseInvoiceInputJson
    {
        [JsonPropertyName("supplier_id")]
        public Guid? SupplierId { get; set; }

        [JsonPropertyName("prepared_employee")]
        public Guid? PreparedEmployee { get; set; }

        [JsonPropertyName("invoice_date")]
        public DateTime? InvoiceDate { get; set; }

        [JsonPropertyName("purchase_order_id")]
        public Guid? PurchaseOrderId { get; set; }

        [JsonPropertyName("totat_item_qty")]
        public int? TotalItemQty { get; set; }

        [JsonPropertyName("total_amount")]
        public int? TotalAmount { get; set; }

        [JsonPropertyName("items")]
        public List<PurchaseInvoiceItemInputJson> Items { get; set; }
    }
}
