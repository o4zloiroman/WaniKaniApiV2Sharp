using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models.Post
{
    public class Review
    {
        [JsonProperty("assignment_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? AssignmentId { get; set; }

        [JsonProperty("subject_id")]
        public int? SubjectId { get; set; }

        [JsonProperty("incorrect_meaning_answers")]
        public int IncorrectMeaningAnswers { get; set; }

        [JsonProperty("incorrect_reading_answers")]
        public int IncorrectReadingAnswers { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }
    }
}
