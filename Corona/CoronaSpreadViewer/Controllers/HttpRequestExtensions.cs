using Microsoft.AspNetCore.Http;
using System;

namespace CoronaSpreadViewer.Controllers {
    public static class HttpRequestExtensions {
        public static Uri GetUri(this HttpRequest request) {
            var builder = new UriBuilder();
            builder.Scheme = request.Scheme;
            builder.Host = request.Host.Value;
            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();
            return builder.Uri;
        }
    }
}
