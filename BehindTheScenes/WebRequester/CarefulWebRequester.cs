using System.Net;

namespace BehindTheScenes.WebRequester
{
    public class CarefulWebRequester : IWebRequester
    {
        private readonly string _uri;

        public CarefulWebRequester(string uri)
        {
            _uri = uri;
        }

        public string MakeRequest()
        {
            var request = WebRequest.Create(_uri);
            try
            {
                using(var response = (HttpWebResponse)request.GetResponse())
                    return $"Request to {_uri} returned {response.StatusCode}.";
            }
            catch(WebException e)
            {
                using(var response = e.Response as HttpWebResponse)
                    if(response != null)
                        return $"Request to {_uri} returned {response.StatusCode}.";
                return $"Request to {_uri} returned no response";
            }
        }
    }
}