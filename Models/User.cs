using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    //https://docs.api.wanikani.com/20170710/?javascript#user
    public class User
    {        
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("max_level_granted_by_subscription")]
        public long MaxLevelGrantedBySubscription { get; set; }

        [JsonProperty("current_vacation_started_at")]
        public object CurrentVacationStartedAt { get; set; }

        [JsonProperty("preferences")]
        public Preferences Preferences { get; set; }
    }
}
