using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Radical : BaseSubject
    {
        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("character_images")]
        public CharacterImages[] CharacterImages { get; set; }
    }
}
