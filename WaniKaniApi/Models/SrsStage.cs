using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class SrsStage
    {
        [JsonProperty("accelerated_interval")]
        public int AcceleratedInterval { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("srs_stage")]
        public int StageId { get; set; }

        [JsonProperty("srs_stage_name")]
        public string SrsStageName { get; set; }
    }
}
