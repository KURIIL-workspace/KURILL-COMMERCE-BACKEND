using KCommerceAPI.Logic.Exception;
using KCommerceAPI.Models.Json.Result;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace KCommerceAPI.Common
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ushort ARGUMENT_EXCEPTION_ERR_CODE = 700;
        private readonly ushort UNHANDLED_EXCEPTION_ERR_CODE = 701;

        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (System.Exception ex)
            {
                Console.Error.Write(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            ErrorResultJson errorResultJson = null;
            int statusCode = 500;

            if (exception is ArgumentException)
            {
                errorResultJson = this.GetErrorResult(ARGUMENT_EXCEPTION_ERR_CODE,
                    exception.Message, exception.InnerException?.Message, exception.GetType().Name);
                statusCode = 400;
            }
            else if (exception is NotFoundException)
            {
                var foundException = (NotFoundException)exception;
                errorResultJson = this.GetErrorResult(foundException.ErrorCode,
                        foundException.Message, exception.InnerException?.Message, foundException.GetType().Name);
                statusCode = 404;
            }
            else if (exception is ApplicationLogicException)
            {
                var foundException = (ApplicationLogicException)exception;
                errorResultJson = this.GetErrorResult(foundException.ErrorCode,
                        foundException.Message, exception.InnerException?.Message, foundException.GetType().Name);
                statusCode = 400;
            }
            else if (exception is AlreadyExistsException)
            {
                var foundException = (AlreadyExistsException)exception;
                errorResultJson = this.GetErrorResult(foundException.ErrorCode,
                        foundException.Message, exception.InnerException?.Message, foundException.GetType().Name);
                statusCode = 400;
            }
            else if (exception is DbUpdateException)
            {
                var foundException = (DbUpdateException)exception;
                var sqlState = foundException.InnerException.Data["SqlState"];

                if (sqlState != null && sqlState.ToString().Trim() == "23503")
                {
                    errorResultJson = this.GetErrorResult(23503, "If you trying to delete an entity, this means that the entity trying to " +
                        "delete having related records. So you are not allowed to delete. But if you trying Add/Update, this means that one " +
                        "of the attribute value you used does not exists in the system. So please check required attribute values are exists before trying " +
                        "again.", "DbUpdateException");
                    statusCode = 400;
                }
                else if (sqlState != null && sqlState.ToString().Trim() == "23505")
                {
                    errorResultJson = this.GetErrorResult(23505, "Seems like you are trying to duplicate some value which " +
                    "are not allowed to duplicate. Values like entity codes (Item Code, Purchase Order Code, etc..), " +
                    "Nic Nos, Epf Nos, etc.. So please re-check the values and retry to insert data", "DbUpdateException");
                    statusCode = 400;
                }
                else
                {
                    errorResultJson = this.GetErrorResult(UNHANDLED_EXCEPTION_ERR_CODE,
                            exception.Message, exception.InnerException?.Message, exception.GetType().Name);
                }
            }
            else
            {
                errorResultJson = this.GetErrorResult(UNHANDLED_EXCEPTION_ERR_CODE,
                            exception.Message, exception.InnerException?.Message, exception.GetType().Name);
            }

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            await response.WriteAsync(JsonSerializer.Serialize<ErrorResultJson>(errorResultJson));
        }

        private ErrorResultJson GetErrorResult(ushort errCode, string message, string exceptionName)
        {
            var errResult = new ErrorResultJson(errCode, message, exceptionName);

            return errResult;
        }

        private ErrorResultJson GetErrorResult(ushort errCode, string message, string innerException, string exceptionName)
        {
            var errResult = new ErrorResultJson(errCode, message, innerException, exceptionName);

            return errResult;
        }
    }
}
