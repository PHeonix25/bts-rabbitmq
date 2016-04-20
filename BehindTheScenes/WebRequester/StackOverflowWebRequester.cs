using System;
using System.Net.Http;
using System.Threading;

namespace BehindTheScenes.WebRequester
{
    public class StackOverflowWebRequester : IWebRequester
    {
        private const int RETRY_COUNT = 3;
        private readonly string _uri;

        public StackOverflowWebRequester(string uri)
        {
            _uri = uri;
        }

        public string MakeRequest()
        {
            for(var i = 1;i <= RETRY_COUNT;++i)
            {
                try
                {
                    var response = new HttpClient().GetAsync(_uri).Result;
                    return $"Requested {_uri}; Response {response.StatusCode}.";
                }
                catch(Exception ex)
                {
                    if(i == RETRY_COUNT)
                        throw;
                    Console.WriteLine($"Retrying WebRequest due to {ex.GetType()}. Retry count: " + i);
                    Thread.Sleep(1000);
                }
            }
            return "No idea how we got here. Please don't show up during demo.";
        }
    }
}