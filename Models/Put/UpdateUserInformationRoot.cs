using Newtonsoft.Json;

namespace WanikaniApi.Models.Put
{
    public class UpdateUserInformationRoot
    {        
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
