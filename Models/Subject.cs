using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WanikaniApi.Models
{
    public class Subject
    {
        [JsonProperty("auxiliary_meanings")]
        public AuxiliaryMeanings[] AuxiliaryMeanings { get; set; }

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
        public Meanings[] Meanings { get; set; }

        [JsonProperty("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; }

        [JsonProperty("lesson_position")]
        public long LessonPosition { get; set; }

        [JsonProperty("readings")]
        public Readings[] Readings { get; set; }

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
        public ContextSentences[] ContextSentences { get; set; }

        [JsonProperty("pronunciation_audios")]
        public PronunciationAudios[] PronunciationAudios { get; set; }

        //[JsonProperty("character_images")]
        //public List<CharacterImages> CharacterImages { get; set; }
    }
}
