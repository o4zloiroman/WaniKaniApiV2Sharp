﻿using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Kanji : Subject, IReadable, ISubject
    {
        [JsonProperty("readings")]
        public Reading[] Readings { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }

        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("visually_similar_subject_ids")]
        public int[] VisuallySimilarSubjectIds { get; set; }
    }
}
