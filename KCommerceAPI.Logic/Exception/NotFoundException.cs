using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Logic.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base("Something not found on the system")
        {
            this.ErrorCode = 1000;
        }

        public NotFoundException(string message) : base(message)
        {
            this.ErrorCode = 1000;
        }

        public NotFoundException(ushort errCode, string message) : base(message) { this.ErrorCode = errCode; }

        public NotFoundException(ushort errCode, string message, System.Exception inner) : base(message, inner) { this.ErrorCode = errCode; }

        protected NotFoundException(ushort errCode,
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { this.ErrorCode = errCode; }

        public ushort ErrorCode { get; }
    }
}
