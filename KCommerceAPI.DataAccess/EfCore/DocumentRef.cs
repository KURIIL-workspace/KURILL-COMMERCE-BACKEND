using System;
using System.Collections.Generic;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class DocumentRef
    {
        public short Id { get; set; }
        public string Type { get; set; } = null!;
        public string Format { get; set; } = null!;
    }
}
