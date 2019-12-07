using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public abstract class Subject
    {
        [JsonProperty("auxiliary_meanings")]
        public AuxiliaryMeaning[] AuxiliaryMeanings { get; set; }

        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public string Characters { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("document_url")]
        public string DocumentUrl { get; set; }

        [JsonProperty("hidden_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? HiddenAt { get; set; }

        [JsonProperty("lesson_position")]
        public int LessonPosition { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("meanings")]
        public Meaning[] Meanings { get; set; }

        [JsonProperty("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
