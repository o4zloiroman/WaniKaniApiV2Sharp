using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class StudyMaterial
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("subject_type")]
        public string SubjectType { get; set; }

        [JsonProperty("meaning_note")]
        public string MeaningNote { get; set; }

        [JsonProperty("reading_note")]
        public string ReadingNote { get; set; }

        [JsonProperty("meaning_synonyms")]
        public string[] MeaningSynonyms { get; set; }
    }
}
