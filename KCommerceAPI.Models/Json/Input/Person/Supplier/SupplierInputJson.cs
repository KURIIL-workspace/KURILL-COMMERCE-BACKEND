using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KCommerceAPI.Models.Json.Input.Contact.Address;

namespace KCommerceAPI.Models.Json.Input.Person.Supplier
{
    public class SupplierInputJson
    {
        [JsonPropertyName("supplier_name")]
        public string? SupplierName { get; set; }

        [JsonPropertyName("supplier_contact")]
        public string? SupplierContact { get; set; }

        [JsonPropertyName("adresses")]
        public List<AddressInputJson> Addresses { get; set; }
    }
}
