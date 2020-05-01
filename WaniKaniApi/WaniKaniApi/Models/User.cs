using System;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class User
    {        
        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("level")]
        public int Level { get; private set; }

        [JsonProperty("profile_url")]
        public string ProfileUrl { get; private set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; private set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; private set; }

        [JsonProperty("current_vacation_started_at")]
        public DateTimeOffset? CurrentVacationStartedAt { get; private set; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }
    }
}
