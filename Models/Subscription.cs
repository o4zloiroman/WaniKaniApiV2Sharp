﻿using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Subscription
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("max_level_granted")]
        public int MaxLevelGranted { get; set; }

        [JsonProperty("period_ends_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset PeriodEndsAt { get; set; }
    }
}
