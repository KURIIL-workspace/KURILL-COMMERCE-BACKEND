using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Logic.Exception
{
    public class ApplicationLogicException:System.Exception
    {
        public ApplicationLogicException(ushort errorCode) { this.ErrorCode = errorCode; }

        public ApplicationLogicException(string message) : base(message) { }

        public ApplicationLogicException(ushort errorCode, string message) : base(message) { this.ErrorCode = errorCode; }

        public ApplicationLogicException(ushort errorCode, string message, System.Exception inner) : base(message, inner) { this.ErrorCode = errorCode; }

        protected ApplicationLogicException(ushort errorCode,
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { this.ErrorCode = errorCode; }

        public ushort ErrorCode { get; } 
    }
}
