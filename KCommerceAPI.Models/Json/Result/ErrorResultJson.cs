using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KCommerceAPI.Models.Json.Result
{
    public class ErrorResultJson
    {
        public ErrorResultJson(ushort errorCode, string message, string exceptionName)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Type = exceptionName;
        }

        public ErrorResultJson(ushort errorCode, string message, string innerException, string exceptionName)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
            this.Type = exceptionName;
            this.InnerException = innerException;
        }

        [JsonPropertyName("error_code")]
        public ushort ErrorCode { get; }

        [JsonPropertyName("message")]
        public string Message { get; }

        [JsonPropertyName("inner_exception")]
        public string InnerException { get; }

        [JsonPropertyName("type")]
        public string Type { get; }
    }
}
