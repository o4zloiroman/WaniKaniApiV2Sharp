using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WaniKaniApi.Models
{
    public class StudyMaterial : ResourceResponse<StudyMaterial>
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("subject_type")]
        public string SubjectType { get; }

        [JsonProperty("meaning_note")]
        public string MeaningNote { get; set; }

        [JsonProperty("reading_note")]
        public string ReadingNote { get; set; }

        [JsonProperty("meaning_synonyms")]
        public List<string> MeaningSynonyms { get; set; }
    }
}
