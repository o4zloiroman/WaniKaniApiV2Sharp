using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Kanji : Subject
    {
        [JsonProperty("readings")]
        public List<Reading> Readings { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("component_subject_ids")]
        public List<int> ComponentSubjectIds { get; set; }

        [JsonProperty("amalgamation_subject_ids")]
        public List<int> AmalgamationSubjectIds { get; set; }

        [JsonProperty("visually_similar_subject_ids")]
        public List<int> VisuallySimilarSubjectIds { get; set; }
    }
}
