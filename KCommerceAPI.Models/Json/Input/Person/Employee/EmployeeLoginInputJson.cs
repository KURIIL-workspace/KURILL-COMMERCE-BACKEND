using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Input.Person.Employee
{
    public class EmployeeLoginInputJson
    {
        [Required]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [Required]
        [JsonPropertyName("employee_id")]
        public Guid? EmployeeId { get; set; }
    }
}
