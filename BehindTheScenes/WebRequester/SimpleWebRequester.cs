using System.Net;

namespace BehindTheScenes.WebRequester
{
    public class SimpleWebRequester : IWebRequester
    {
        private readonly string _uri;

        public SimpleWebRequester(string uri)
        {
            _uri = uri;
        }

        public string MakeRequest()
        {
            var webRequest = WebRequest.Create(_uri);
            using(var response = (HttpWebResponse)webRequest.GetResponse())
                return $"Requested {webRequest.RequestUri}; Response {response.StatusCode}.";
        }
    }
}