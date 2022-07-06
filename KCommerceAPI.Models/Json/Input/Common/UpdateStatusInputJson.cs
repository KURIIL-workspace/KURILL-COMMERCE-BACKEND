using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Common
{
    public class UpdateStatusInputJson
    {
        [Required]
        [JsonPropertyName("status")]
        public short Status { get; set; }

        [JsonPropertyName("employee_id")]
        public Guid? EmployeeId { get; set; }

        [JsonPropertyName("performed_date")]
        public DateTime? PerformedDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class UpdateStockReadingAllStatusInputJson : UpdateStatusInputJson
    {
        [JsonPropertyName("stock_reading_ids")]
        public List<Guid> StockReadingIds { get; set; }
    }
}
