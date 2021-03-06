﻿using System;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class LevelProgression : ResourceResponse<LevelProgression>
    {
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("unlocked_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UnlockedAt { get; set; }

        [JsonProperty("started_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("passed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PassedAt { get; set; }

        [JsonProperty("completed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("abandoned_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? AbandonedAt { get; set; }
    }
}
