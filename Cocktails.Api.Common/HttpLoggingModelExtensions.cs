using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktails.Api.Common
{
    internal static class HttpLoggingModelExtensions
    {
        public static async Task<HttpLoggingModel> SetRequest(this HttpLoggingModel model, HttpRequest request)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = request.Body;
            await request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[requestBodyStream.Length];
            await requestBodyStream.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            request.Body = requestBodyStream;

            model.HttpMethod = request.Method;
            model.QueryString = UriHelper.GetDisplayUrl(request);
            model.Headers = request.Headers.ToDictionary(a => a.Key, a => a.Value.AsEnumerable());
            model.RequestBody = bodyAsText;

            return model;
        }

        public static async Task<HttpLoggingModel> SetResponse(this HttpLoggingModel model, Stream responseStream, int statusCode)
        {
            responseStream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[responseStream.Length];
            await responseStream.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            model.ResponseBody = bodyAsText;
            model.StatusCode = statusCode;

            return model;
        }

        public static HttpLoggingModel SetException(this HttpLoggingModel model, Exception exception)
        {
            model.RequestBody = JsonConvert.SerializeObject(exception);
            model.StatusCode = 500;

            return model;
        }

        public static HttpLoggingModel SetTiming(this HttpLoggingModel model, DateTimeOffset startTime, long duration)
        {
            model.StartTime = startTime;
            model.Duration = duration;

            return model;
        }
    }
}
