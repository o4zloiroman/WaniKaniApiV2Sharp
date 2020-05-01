using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Summary
    {
        [JsonProperty("lessons")]
        public List<Lesson> Lessons { get; set; }

        [JsonProperty("next_reviews_at")]
        public DateTimeOffset? NextReviewsAt { get; set; }

        [JsonProperty("reviews")]
        public List<Lesson> Reviews { get; set; }
    }
}