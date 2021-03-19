using Microsoft.AspNetCore.Http;
using System;

namespace CoronaSpreadViewer.Controllers {
    public static class HttpRequestExtensions {
        public static Uri GetUri(this HttpRequest request) {
            var uri = $"{request.Scheme}://{request.Host.Value}{request.Path}";
            return new Uri(uri);
        }
    }
}
