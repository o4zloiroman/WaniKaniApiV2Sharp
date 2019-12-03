﻿using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Preferences
    {
        [JsonProperty("default_voice_actor_id", NullValueHandling = NullValueHandling.Ignore)]
        public int DefaultVoiceActorId { get; set; }
        
        [JsonProperty("lessons_autoplay_audio", NullValueHandling = NullValueHandling.Ignore)]
        public bool LessonsAutoplayAudio { get; set; }

        [JsonProperty("lessons_batch_size", NullValueHandling = NullValueHandling.Ignore)]
        public int LessonsBatchSize { get; set; }
        
        [JsonProperty("lessons_presentation_order", NullValueHandling = NullValueHandling.Ignore)]
        public string LessonsPresentationOrder { get; set; }

        [JsonProperty("reviews_autoplay_audio", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReviewsAutoplayAudio { get; set; }

        [JsonProperty("reviews_display_srs_indicator", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReviewsDisplaySrsIndicator { get; set; }
    }
}
