using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WanikaniApi.Models
{
    public class Assignment
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; private set;  }

        [JsonProperty("subject_id")]
        public int SubjectId { get; private set; }

        public Lazy<Subject> Subject => new Lazy<Subject>(() => WaniKaniClient.GetSubject(SubjectId));
        
        [JsonProperty("subject_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SubjectType SubjectType { get; private set; }
        
        [JsonProperty("srs_stage")]
        public int SrsStage { get; set; }

        [JsonProperty("srs_stage_name")]
        public string SrsStageName { get; set; }

        [JsonProperty("unlocked_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UnlockedAt { get; private set; }

        [JsonProperty("started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartedAt { get; private set; }

        [JsonProperty("passed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? PassedAt { get; private set; }

        [JsonProperty("burned_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? BurnedAt { get; private set; }

        [JsonProperty("available_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? AvailableAt { get; private set; }

        [JsonProperty("resurrected_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ResurrectedAt { get; private set; }
        
        [JsonProperty("passed")]
        public bool Passed { get; private set;  }
        
        [JsonProperty("hidden")]
        public bool Hidden { get; private set; }
    }
}
