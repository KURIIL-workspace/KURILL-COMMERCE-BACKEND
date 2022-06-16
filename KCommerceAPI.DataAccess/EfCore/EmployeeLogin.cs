using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class EmployeeLogin
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
