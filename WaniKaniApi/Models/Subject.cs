using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WaniKaniApi.Models
{
    public class Subject
    {
        [JsonProperty("auxiliary_meanings")]
        public List<AuxiliaryMeaning> AuxiliaryMeanings { get; set; }

        [JsonProperty("characters")]
        public string Characters { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("document_url")]
        public string DocumentUrl { get; set; }

        [JsonProperty("hidden_at")]
        public DateTimeOffset? HiddenAt { get; set; }

        [JsonProperty("lesson_position")]
        public int LessonPosition { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("meanings")]
        public List<Meaning> Meanings { get; set; }

        [JsonProperty("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
