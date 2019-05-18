using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class SubjectBase
    {
        [JsonProperty("auxiliary_meanings")]
        public AuxiliaryMeanings[] AuxiliaryMeanings { get; set; }

        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public string Characters { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("document_url")]
        public Uri DocumentUrl { get; set; }

        [JsonProperty("hidden_at")]
        public object HiddenAt { get; set; }

        [JsonProperty("lesson_position")]
        public long LessonPosition { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; }

        [JsonProperty("meanings")]
        public Meanings[] Meanings { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
