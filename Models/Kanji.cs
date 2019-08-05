using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    class Kanji : BaseSubject, IReadable
    {
        [JsonProperty("readings")]
        public Readings[] Readings { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }

        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("visually_similar_subject_ids")]
        public object[] VisuallySimilarSubjectIds { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }
    }
}
