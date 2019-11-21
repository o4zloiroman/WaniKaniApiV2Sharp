using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Radical : Subject
    {
        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("character_images")]
        public CharacterImage[] CharacterImages { get; set; }
    }
}
