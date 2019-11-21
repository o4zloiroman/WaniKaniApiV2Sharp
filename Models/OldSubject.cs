using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WanikaniApi.Models
{
    [Obsolete]
    public class OldSubject : ResourceResponse<OldSubject>
    {
        [JsonProperty("auxiliary_meanings")]
        public AuxiliaryMeaning[] AuxiliaryMeanings { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("hidden_at")]
        public object HiddenAt { get; set; }

        [JsonProperty("document_url")]
        public Uri DocumentUrl { get; set; }

        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public string Characters { get; set; }

        [JsonProperty("meanings")]
        public Meaning[] Meanings { get; set; }

        [JsonProperty("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; }

        [JsonProperty("lesson_position")]
        public long LessonPosition { get; set; }

        [JsonProperty("readings")]
        public Reading[] Readings { get; set; }

        [JsonProperty("component_subject_ids")]
        public int[] ComponentSubjectIds { get; set; }

        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("visually_similar_subject_ids")]
        public object[] VisuallySimilarSubjectIds { get; set; }

        [JsonProperty("parts_of_speech")]
        public string[] PartsOfSpeech { get; set; }

        [JsonProperty("reading_mnemonic")]
        public string ReadingMnemonic { get; set; }

        [JsonProperty("context_sentences")]
        public ContextSentence[] ContextSentences { get; set; }

        [JsonProperty("pronunciation_audios")]
        public PronunciationAudio[] PronunciationAudios { get; set; }

        [JsonProperty("character_images")]
        public List<CharacterImage> CharacterImages { get; set; }
    }
}
