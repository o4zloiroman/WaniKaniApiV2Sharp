using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Pages
    {
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }

        [JsonProperty("previous_url")]
        public object PreviousUrl { get; set; }
    }
}
