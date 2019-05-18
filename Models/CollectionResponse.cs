using System.Collections.Generic;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class CollectionResponse<T> : BaseResponse<List<T>>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("pages")]
        public Pages Pages { get; set; }
    }
}
