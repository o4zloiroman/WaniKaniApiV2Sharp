using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Review
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("assignment_id")]
        public int? AssignmentId { get; set; }

        [JsonProperty("subject_id")]
        public int? SubjectId { get; set; }

        [JsonProperty("starting_srs_stage")]
        public int? StartingSrsStage { get; }

        [JsonProperty("starting_srs_stage_name")]
        public string StartingSrsStageName { get; private set; }

        [JsonProperty("ending_srs_stage")]
        public int EndingSrsStage { get; set; }

        [JsonProperty("ending_srs_stage_name")]
        public string EndingSrsStageName { get; private set; }

        [JsonProperty("incorrect_meaning_answers")]
        public int IncorrectMeaningAnswers { get; set; }

        [JsonProperty("incorrect_reading_answers")]
        public int IncorrectReadingAnswers { get; set; }
    }
}
