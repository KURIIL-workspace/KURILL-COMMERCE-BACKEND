using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Person.Employee
{
    public class EmployeeInputJson
    {
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("birth_date")]
        public DateOnly? BirthDate { get; set; }

        [JsonPropertyName("contact")]
        public string? Contact { get; set; }

        [JsonPropertyName("employee_status")]
        public short? EmployeeStatus { get; set; }
    }
}
