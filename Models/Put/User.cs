using Newtonsoft.Json;

namespace WanikaniApi.Models.Put
{
    public class User
    {
        [JsonProperty("preferences")]
        public Models.Preferences Preferences { get; set; }
    }
}
