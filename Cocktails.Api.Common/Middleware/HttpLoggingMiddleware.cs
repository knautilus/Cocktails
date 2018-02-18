using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Cocktails.Api.Common.Middleware
{
    public class HttpLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HttpLoggingMiddleware(RequestDelegate next
            , ILoggerFactory loggerFactory
            )
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<HttpLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var loggingModel = new HttpLoggingModel();
            var startTime = DateTimeOffset.Now;
            var sw = Stopwatch.StartNew();

            var responseBodyStream = new MemoryStream();
            var originalResponseBody = context.Response.Body;
            context.Response.Body = responseBodyStream;

            try
            {
                await loggingModel.SetRequest(context.Request);

                await _next(context);

                await loggingModel.SetResponse(responseBodyStream, context.Response.StatusCode);

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBody);
            }
            catch (Exception ex)
            {
                loggingModel.SetException(ex);
                throw;
            }
            finally
            {
                context.Response.Body = originalResponseBody;

                sw.Stop();
                loggingModel.SetTiming(startTime, sw.ElapsedMilliseconds);

                _logger.LogInformation(JsonConvert.SerializeObject(loggingModel));
            }
        }
    }
}
