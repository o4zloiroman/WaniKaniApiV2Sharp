using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class BaseResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("data_updated_at")]
        public DateTimeOffset DataUpdatedAt { get; set; }        
    }
}
