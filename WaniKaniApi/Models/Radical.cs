using System.Collections.Generic;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Radical : Subject
    {
        [JsonProperty("amalgamation_subject_ids")]
        public List<int> AmalgamationSubjectIds { get; set; }

        [JsonProperty("character_images")]
        public List<CharacterImage> CharacterImages { get; set; }
    }
}
