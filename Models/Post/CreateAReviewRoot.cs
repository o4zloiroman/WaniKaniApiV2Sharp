using Newtonsoft.Json;

namespace WanikaniApi.Models.Post
{
    public class CreateAReviewRoot
    {        
        [JsonProperty("review")]
        public Review Review { get; set; }
    }
}
