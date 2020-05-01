using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Lesson
    {
        [JsonProperty("available_at")]
        public DateTimeOffset AvailableAt { get; set; }

        [JsonProperty("subject_ids")]
        public List<int> SubjectIds { get; set; }
    }
}
