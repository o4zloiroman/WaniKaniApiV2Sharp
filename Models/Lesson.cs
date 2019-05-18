using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Lesson
    {
        [JsonProperty("available_at")]
        public DateTimeOffset AvailableAt { get; set; }

        [JsonProperty("subject_ids")]
        public int[] SubjectIds { get; set; }
    }
}
