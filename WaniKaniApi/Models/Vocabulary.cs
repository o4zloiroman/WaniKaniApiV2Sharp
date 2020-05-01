using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Vocabulary : Subject
    {
        [JsonProperty("readings")]
        public List<Reading> Readings { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("component_subject_ids")]
        public List<int> ComponentSubjectIds { get; set; }        

        [JsonProperty("parts_of_speech")]
        public List<string> PartsOfSpeech { get; set; }

        [JsonProperty("context_sentences")]
        public List<ContextSentence> ContextSentences { get; set; }

        [JsonProperty("pronunciation_audios")]
        public List<PronunciationAudio> PronunciationAudios { get; set; }
    }
}
