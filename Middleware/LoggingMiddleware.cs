using System.Diagnostics;

namespace StudentCoursesSystem.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate request, ILogger<LoggingMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestBodyStream = new StreamReader(context.Request.Body);
            string requestBody = await requestBodyStream.ReadToEndAsync();
            context.Request.Body.Position = 0; // Reset the stream position for further use

            _logger.LogInformation("The Request Pipeline started........");
            _logger.LogDebug($"REQUEST: {requestBody}");
            _logger.LogDebug($"The Request Metadata is -----> Method: {context.Request.Method}" +
                $", Headers: {context.Request.Headers}");

            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _request.Invoke(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            _logger.LogDebug($"RESPONSE: StatusCode={context.Response.StatusCode}, Body={responseBody}");
            

            await responseBodyStream.CopyToAsync(originalBodyStream);
        }
    }
}
