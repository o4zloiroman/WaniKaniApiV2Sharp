using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WaniKaniApi.Singleton
{
    internal static class HttpClientSingleton
    {
        private static object Lock { get; } = new object();

        private static HttpClient Instance { get; set; }

        internal static HttpClient GetInstance()
        {
            lock(Lock) 
            {
                if (Instance != null) return Instance;
                
                Instance = new HttpClient();

                return Instance;
            }
        }
        
        public static HttpClient GetInstance(string apiToken)
        {
            lock(Lock) 
            {
                if (Instance != null || string.IsNullOrEmpty(apiToken)) return Instance;
                
                Instance = new HttpClient();
                
                Instance.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                Instance.DefaultRequestHeaders.Add("Wanikani-Revision", "20170710");
                Instance.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiToken);

                return Instance;
            }
        }
        
        internal static async Task<T> CustomGet<T>(string apiEndpointPath)
        {
            var response = await GetInstance().GetAsync(apiEndpointPath);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        internal static async Task<T> CustomGet<T>(string apiEndpointPath, Dictionary<string, string> parameters)
        {
            var query = string.Join('&', parameters.Where(pair => !string.IsNullOrEmpty(pair.Value))
                .Select(pair => $"{pair.Key}={pair.Value}"));
            query = $"{apiEndpointPath}?{query}";
            
            var response = await GetInstance().GetAsync(query);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        internal static async Task CustomPost(string apiEndpointPath, string data)
        {
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await GetInstance().PostAsync(apiEndpointPath, content);
            response.EnsureSuccessStatusCode();
        }

        internal static async Task CustomPut(string apiEndpointPath, string data)
        {
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await GetInstance().PutAsync(apiEndpointPath, content);
            response.EnsureSuccessStatusCode();
        }
    }
}