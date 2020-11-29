using System;
using System.Net.Http;
using ShopWebApp.Constants;

namespace ShopWebApp.Common
{
    public static class HttpClientAccessor
    {
        public static Func<HttpClient> ValueFactory = () => {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Constant.BASE_ADDRESS);
            return client;
        };

        private readonly static Lazy<HttpClient> client = new Lazy<HttpClient>(ValueFactory);

        public static HttpClient HttpClient
        {
            get { return client.Value; }
        }
    }
}
