using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class ResourceResponse<T> : BaseResponse<T>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
