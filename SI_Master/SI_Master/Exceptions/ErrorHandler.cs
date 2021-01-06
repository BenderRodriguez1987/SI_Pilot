using Refit;
using SI_Master.Models;
using SI_Master.REST;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.Exceptions
{
    class ErrorHandler
    {
        public const string ERROR_MESSAGE = "errorMessage";

        public static Answer HandleException(Exception exception)
        {
            Debug.WriteLine(exception);
            var response = new Answer()
            {
                ResType = 1
            };
            if (exception.GetType() == typeof(TaskCanceledException) || (exception.InnerException != null && exception.InnerException.GetType() == typeof(TaskCanceledException)))
            {
                response.ResType = NetworkService.NO_CONNECTION;
                response.ResMsg = "Timeout";
            }
            else if (exception.GetType() == typeof(ServerErrorException))
            {
                response.ResType = NetworkService.SERVER_ERROR;
                response.ResMsg = exception.Message;
            }
            else if (exception.GetType() != typeof(InternalException))
            {
                if (string.IsNullOrEmpty(exception.Message)) {
                    response.ResMsg = "Internal error";
                }
                else {
                    response.ResMsg = exception.Message;
                }
            }
            else if (exception.GetType() == typeof(ApiException)) {
                response.ResType = NetworkService.API_ERROR;
                response.ResMsg = exception.Message;
            }
            else {
                response.ResMsg = exception.Message;
            }
            return response;
        }
    }
}
