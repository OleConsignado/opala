using System;
using System.Net;

namespace Opala.Infra.NotificationAdapter.Exceptions
{
    public class NotificationAdapterException : Exception
    {
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public NotificationAdapterException()
            : base()
        {
        }

        public NotificationAdapterException(string responseMessage, HttpStatusCode statusCode)
            : base()
        {
            ResponseMessage = responseMessage;
            StatusCode = statusCode;
        }
    }
}
