using KCommerceAPI.Models.Json.Input.Contact.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Person.Customer
{
    public class CustomerInputJson
    {
        [JsonPropertyName("cuustomer_name")]
        public string? CustomerName { get; set; }
        [JsonPropertyName("customer_contact")]
        public string? CustomerContact { get; set; }


        [JsonPropertyName("addresses")]
        public ICollection<AddressInputJson> Addresses { get; set; }
    }
}
