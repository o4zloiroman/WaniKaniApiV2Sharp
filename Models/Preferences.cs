using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Preferences
    {
        [JsonProperty("default_voice_actor_id")]
        public int DefaultVoiceActorId { get; set; }
        
        [JsonProperty("lessons_autoplay_audio")]
        public bool LessonsAutoplayAudio { get; set; }

        [JsonProperty("lessons_batch_size")]
        public int LessonsBatchSize { get; set; }
        
        [JsonProperty("lessons_presentation_order")]
        public LessonsPresentationOrder LessonsPresentationOrder { get; set; }

        [JsonProperty("reviews_autoplay_audio")]
        public bool ReviewsAutoplayAudio { get; set; }

        [JsonProperty("reviews_display_srs_indicator")]
        public bool ReviewsDisplaySrsIndicator { get; set; }
    }
}
