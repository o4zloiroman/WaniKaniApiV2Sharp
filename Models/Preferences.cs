using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    //https://docs.api.wanikani.com/20170710/?javascript#get-user-information
    public class Preferences
    {
        [JsonProperty("lessons_batch_size", NullValueHandling = NullValueHandling.Ignore)]
        public int LessonsBatchSize { get; set; }

        [JsonProperty("lessons_autoplay_audio", NullValueHandling = NullValueHandling.Ignore)]
        public bool LessonsAutoplayAudio { get; set; }

        [JsonProperty("reviews_autoplay_audio", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReviewsAutoplayAudio { get; set; }

        [JsonProperty("lessons_presentation_order", NullValueHandling = NullValueHandling.Ignore)]
        public string LessonsPresentationOrder { get; set; }

        [JsonProperty("reviews_display_srs_indicator", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReviewsDisplaySrsIndicator { get; set; }
    }
}
