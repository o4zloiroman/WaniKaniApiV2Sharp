using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Summary : BaseResponse<Summary>
    {
        [JsonProperty("lessons")]
        public Lesson[] Lessons { get; set; }

        [JsonProperty("next_reviews_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset NextReviewsAt { get; set; }

        [JsonProperty("reviews")]
        public Lesson[] Reviews { get; set; }
    }
}