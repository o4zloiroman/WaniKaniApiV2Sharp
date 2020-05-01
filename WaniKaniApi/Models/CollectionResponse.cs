using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WaniKaniApi.Singleton;

namespace WaniKaniApi.Models
{
    [JsonObject]
    public class CollectionResponse<T> : BaseResponse<IEnumerable<ResourceResponse<T>>>, IEnumerable<ResourceResponse<T>>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("pages")]
        public Pages Pages { get; set; }

        public async Task<IEnumerable<ResourceResponse<T>>> GetNextPage()
        {
            if (string.IsNullOrEmpty(Pages.NextUrl)) return null;
            
            var apiEndpointPath = Pages.NextUrl.Replace("https://api.wanikani.com/v2/", "");
            return await HttpClientSingleton.CustomGet<CollectionResponse<T>>(apiEndpointPath);
        }

        public async Task<IEnumerable<ResourceResponse<T>>> GetPreviousPage()
        {
            if (string.IsNullOrEmpty(Pages.PreviousUrl)) return null;
            
            var apiEndpointPath = Pages.PreviousUrl.Replace("https://api.wanikani.com/v2/", "");
            return await HttpClientSingleton.CustomGet<CollectionResponse<T>>(apiEndpointPath);
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
