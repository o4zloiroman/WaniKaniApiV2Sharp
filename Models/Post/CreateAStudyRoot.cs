using Newtonsoft.Json;

namespace WanikaniApi.Models.Post
{
    public class CreateAStudyRoot
    {        
        [JsonProperty("study_material")]
        public StudyMaterial StudyMaterial { get; set; }
    }
}
