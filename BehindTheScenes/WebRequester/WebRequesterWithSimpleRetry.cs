using System;
using System.Net;

using Polly;
using Polly.Retry;

namespace BehindTheScenes.WebRequester
{
    public class WebRequesterWithSimpleRetry : IWebRequester
    {
        private readonly RetryPolicy _retryPolicy;
        private readonly string _uri;

        public WebRequesterWithSimpleRetry(string uri)
        {
            _uri = uri;
            _retryPolicy = Policy.Handle<WebException>().Retry(3,
                (exception, i) => Console.WriteLine($"{exception.GetType()} thrown, retry count {i}"));
        }

        public string MakeRequest()
            => _retryPolicy.Execute(() =>
            {
                var webRequest = WebRequest.Create(_uri);
                using(var response = (HttpWebResponse)webRequest.GetResponse())
                    return $"Requested {_uri}; Response {response.StatusCode}.";
            });
    }
}