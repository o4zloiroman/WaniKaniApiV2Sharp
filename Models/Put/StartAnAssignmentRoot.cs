using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models.Put
{
    public class StartAnAssignmentRoot
    {        
        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }
    }
}
