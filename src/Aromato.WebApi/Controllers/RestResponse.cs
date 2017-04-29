using Newtonsoft.Json;

namespace Aromato.WebApi.Controllers
{
    public class RestResponse
    {
        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}