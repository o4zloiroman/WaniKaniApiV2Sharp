using System.Collections.Generic;
using Newtonsoft.Json;

namespace WanikaniApi.Models.Post
{
    public class StudyMaterial
    {
        [JsonProperty("subject_id")]
        public long SubjectId { get; set; }

        [JsonProperty("meaning_note", NullValueHandling = NullValueHandling.Ignore)]
        public string MeaningNote { get; set; }

        [JsonProperty("reading_note", NullValueHandling = NullValueHandling.Ignore)]
        public string ReadingNote { get; set; }

        [JsonProperty("meaning_synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MeaningSynonyms { get; set; }
    }
}
