using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeLogins = new HashSet<EmployeeLogin>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? BirthDate { get; set; }

        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }
    }
}
