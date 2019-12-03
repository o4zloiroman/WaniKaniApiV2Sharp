using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Assignment
    {
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreatedAt { get; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; }

        public Lazy<Subject> Subject => new Lazy<Subject>(() => WaniKaniClient.GetSubject(SubjectId));

        [JsonProperty("subject_type")]
        public string SubjectType { get; }
        
        [JsonProperty("srs_stage")]
        public int SrsStage { get; set; }

        [JsonProperty("srs_stage_name")]
        public string SrsStageName { get; set; }

        [JsonProperty("unlocked_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UnlockedAt { get; set; }

        [JsonProperty("started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartedAt { get; set; }

        [JsonProperty("passed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? PassedAt { get; }

        [JsonProperty("burned_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? BurnedAt { get; }

        [JsonProperty("available_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? AvailableAt { get; set; }

        [JsonProperty("resurrected_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ResurrectedAt { get; set; }
        
        [JsonProperty("passed")]
        public bool Passed { get; }
        
        [JsonProperty("hidden")]
        public bool Hidden { get; }
    }
}
