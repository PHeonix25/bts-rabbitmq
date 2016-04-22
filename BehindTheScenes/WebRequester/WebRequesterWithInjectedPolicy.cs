using System.Net;

using Polly.Retry;

namespace BehindTheScenes.WebRequester
{
    public class WebRequesterWithPolly : IWebRequester
    {
        private readonly RetryPolicy _retryPolicy;
        private readonly string _uri;

        public WebRequesterWithPolly(string uri, RetryPolicy policy)
        {
            _uri = uri;
            _retryPolicy = policy;
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