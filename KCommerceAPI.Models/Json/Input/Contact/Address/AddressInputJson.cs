using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Contact.Address
{
    public class AddressInputJson
    {
        [JsonPropertyName("address_line_1")]
        public string? AddLine1 { get; set; }

        [JsonPropertyName("address_line_2")]
        public string? AddLine2 { get; set; }

        [JsonPropertyName("postal_code")]
        public int? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("supplier")]
        public Guid? Supplier { get; set; }

        [JsonPropertyName("employee")]
        public Guid? Employee { get; set; }

        [JsonPropertyName("customer")]
        public Guid? Customer { get; set; }
    }
}
