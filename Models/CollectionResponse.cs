using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    [JsonObject]
    public class CollectionResponse<T> : BaseResponse<IEnumerable<ResourceResponse<T>>>, IEnumerable<ResourceResponse<T>>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("pages")]
        public Pages Pages { get; set; }

        public CollectionResponse<T> GetNextPage()
        {
            if (string.IsNullOrEmpty(Pages.NextUrl)) return null;
            
            var apiEndpointPath = Pages.NextUrl.Replace("https://api.wanikani.com/v2/", "");
            var json = WaniKaniClient.CustomGet(apiEndpointPath);
            return JsonConvert.DeserializeObject<CollectionResponse<T>>(json);
        }

        public CollectionResponse<T> GetPreviousPage()
        {
            if (string.IsNullOrEmpty(Pages.PreviousUrl)) return null;
            
            var apiEndpointPath = Pages.PreviousUrl.Replace("https://api.wanikani.com/v2/", "");
            var json = WaniKaniClient.CustomGet(apiEndpointPath);
            return JsonConvert.DeserializeObject<CollectionResponse<T>>(json); 
        }
        
        public IEnumerator<ResourceResponse<T>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
