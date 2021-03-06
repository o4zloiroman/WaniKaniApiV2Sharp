﻿using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class PronunciationAudio
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
