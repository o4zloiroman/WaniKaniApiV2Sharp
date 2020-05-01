using System;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Pages
    {
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("next_url")]
        public string NextUrl { get; set; }

        [JsonProperty("previous_url")]
        public string PreviousUrl { get; set; }
    }
}
