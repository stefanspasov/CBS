namespace CBS.Logic.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using CBS.Logic.Loggers;

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public ExceptionFilter(ILogger logger)
        {
            this.logger = logger;
        }

        private static readonly Dictionary<Type, HttpStatusCode> ResponseCodes = new Dictionary<Type, HttpStatusCode>{
                     { typeof(NotImplementedException), HttpStatusCode.NotImplemented },
                     { typeof(Exception), HttpStatusCode.InternalServerError},
                     { typeof(ArgumentException), HttpStatusCode.BadRequest },
                     { typeof(ConfigurationException), HttpStatusCode.InternalServerError }
                 };

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var exception = actionExecutedContext.Exception;
            var exceptionType = actionExecutedContext.Exception.GetType();

            if (ResponseCodes.ContainsKey(exceptionType))
            {
                statusCode = ResponseCodes[exceptionType];
            }

            this.logger.LogError("Error detected.", exception);

            actionExecutedContext.Response = 
                actionExecutedContext.Request.CreateErrorResponse(statusCode, exception.Message);
        }
    }
}
