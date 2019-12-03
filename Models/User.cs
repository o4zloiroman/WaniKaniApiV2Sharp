using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    //https://docs.api.wanikani.com/20170710/#user
    public class User
    {        
        [JsonProperty("id")]
        public Guid Id { get; }

        [JsonProperty("username")]
        public string Username { get; }

        [JsonProperty("level")]
        public int Level { get; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; }

        [JsonProperty("max_level_granted_by_subscription")]
        public long MaxLevelGrantedBySubscription { get; }

        [JsonProperty("current_vacation_started_at")]
        public object CurrentVacationStartedAt { get; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }
    }
}
