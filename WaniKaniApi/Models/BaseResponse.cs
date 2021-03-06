﻿using System;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class BaseResponse<T>
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("data_updated_at")]
        public DateTimeOffset? DataUpdatedAt { get; set; }
        
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
