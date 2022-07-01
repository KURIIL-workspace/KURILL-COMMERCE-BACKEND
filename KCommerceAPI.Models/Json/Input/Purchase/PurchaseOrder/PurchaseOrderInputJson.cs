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
        [JsonPropertyName("order_date")]
        public DateTime? OrderDate { get; set; }
        
        [JsonPropertyName("supplier_id")]
        public Guid? SupplierId { get; set; }

        [JsonIgnore]
        public int? TotalQty {
            get 
            {
                return this.Items.Sum(c => c.Qty);
            } 
        }
        
        [JsonPropertyName("terms_condition")]
        public string? TermsCondition { get; set; }
        
        [JsonPropertyName("ship_to_address")]
        public string? ShipToAddress { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
       
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }
        
        [JsonPropertyName("prepared_employee")]
        public Guid? PreparedEmployee { get; set; }

        [JsonPropertyName("items")]
        public List<PurchaseOrderItemInputJson> Items { get; set; }

    }
}
