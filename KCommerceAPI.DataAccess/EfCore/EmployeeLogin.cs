using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class EmployeeLogin
    {
        public string UserName { get; set; } = null!;
        public Guid? EmployeeId { get; set; }
        public Guid Id { get; set; }
        public string? Password { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
