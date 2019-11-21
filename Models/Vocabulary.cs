using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Vocabulary : Subject, IReadable
    {
        [JsonProperty("readings")]
        public Reading[] Readings { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }        

        [JsonProperty("parts_of_speech")]
        public string[] PartsOfSpeech { get; set; }

        [JsonProperty("context_sentences")]
        public ContextSentence[] ContextSentences { get; set; }

        [JsonProperty("pronunciation_audios")]
        public PronunciationAudio[] PronunciationAudios { get; set; }
    }
}
