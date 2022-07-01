using System.Text.Json.Serialization;

namespace KCommerceAPI.Models.Json.Input.Purchase.PurchaseOrder
{
    public class PurchaseOrderItemInputJson
    {
        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("qty")]
        public int? Qty { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal? UnitPrice { get; set; }
    }
}