using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    class Kanji : SubjectBase
    {
        [JsonProperty("readings")]
        public Readings[] Readings { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }

        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("visually_similar_subject_ids")]
        public object[] VisuallySimilarSubjectIds { get; set; }
    }
}
