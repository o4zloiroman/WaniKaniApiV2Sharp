using System.Collections.Generic;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public abstract class CollectionResponse<T> : ResourceResponse<List<T>>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("pages")]
        public Pages Pages { get; set; }
    }
}
