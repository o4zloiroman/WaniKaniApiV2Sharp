using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public abstract class ResourceResponse<T> : BaseResponse<T>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
