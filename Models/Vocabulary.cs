using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Vocabulary : BaseSubject, IReadable
    {
        [JsonProperty("readings")]
        public Readings[] Readings { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }        

        [JsonProperty("parts_of_speech")]
        public string[] PartsOfSpeech { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("context_sentences")]
        public ContextSentences[] ContextSentences { get; set; }

        [JsonProperty("pronunciation_audios")]
        public PronunciationAudios[] PronunciationAudios { get; set; }
    }
}
