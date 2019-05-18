using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Reviews : Review
    {
        [JsonProperty("ending_srs_stage")]
        public int EndingSrsStage { get; set; }

        [JsonProperty("ending_srs_stage_name")]
        public string EndingSrsStageName { get; set; }
    }
}
