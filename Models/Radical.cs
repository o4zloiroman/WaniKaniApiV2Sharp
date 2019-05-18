using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    class Radical : SubjectBase
    {
        [JsonProperty("amalgamation_subject_ids")]
        public int[] AmalgamationSubjectIds { get; set; }

        [JsonProperty("character_images")]
        public CharacterImages CharacterImages { get; set; }
    }
}
