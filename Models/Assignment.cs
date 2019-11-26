using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Assignment : ResourceResponse<Assignment>
    {
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        public Lazy<ISubject> Subject => new Lazy<ISubject>(() => WaniKaniClient.GetSubject(SubjectId));

        [JsonProperty("subject_type")]
        public string SubjectType { get; set; }

        [JsonProperty("srs_stage")]
        public int SrsStage { get; set; }

        [JsonProperty("srs_stage_name")]
        public string SrsStageName { get; set; }

        [JsonProperty("unlocked_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset UnlockedAt { get; set; }

        [JsonProperty("started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("passed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset PassedAt { get; set; }

        [JsonProperty("burned_at")]
        public DateTimeOffset BurnedAt { get; set; }

        [JsonProperty("available_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset AvailableAt { get; set; }

        [JsonProperty("resurrected_at")]
        public DateTimeOffset ResurrectedAt { get; set; }

        [JsonProperty("passed")]
        public bool Passed { get; set; }

        [JsonProperty("resurrected")]
        public bool Resurrected { get; set; }
    }
}
