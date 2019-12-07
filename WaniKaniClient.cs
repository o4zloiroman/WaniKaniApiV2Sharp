using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WanikaniApi.Models;

namespace WanikaniApi
{
    public static class WaniKaniClient
    {
        private const string And = "&";
        private static string _apiToken;
        private static readonly Regex ApiRegex = new Regex("^[a-f0-9-]{36}$");

        /// <summary>
        /// API V2 token. 
        /// </summary>
        public static string ApiToken
        {
            get => _apiToken;
            set
            {
                if (ApiRegex.IsMatch(value))
                    _apiToken = value;
                else
                {
                    throw new ArgumentException("Invalid API token.");
                }
            }
        }
        
        /// <summary>
        /// Mark the assignment as started, moving the assignment from the lessons to the review queue. 
        /// </summary>
        /// <param name="id">Unique identifier of the assignment.</param>
        public static void StartAssignment(int id)
        {
            var startAssignment = new
            {
                started_at = DateTime.UtcNow
            };

            var data = JsonConvert.SerializeObject(startAssignment);

            CustomPut($"assignments/{id}/start", data);
        }

        /// <summary>
        /// Returns an updated summary of user information.
        /// </summary>
        public static void UpdateUserInformation (Preferences preferences)
        {
            var updateUserInformation = new
            {
                user = new
                {
                    preferences
                }
            };

            var data = JsonConvert.SerializeObject(updateUserInformation);

            CustomPut("user", data);
        }

        /// <summary>
        /// Updates a study material for a specific Id.
        /// </summary>
        public static void UpdateStudyMaterial(StudyMaterial studyMaterial)
        {
            var createAStudyMaterial = new
            {
                study_material = studyMaterial
            };

            var data = JsonConvert.SerializeObject(createAStudyMaterial);

            CustomPut("study_materials", data);
        }

        /// <summary>
        /// Returns a collection of all assignments, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public static CollectionResponse<Assignment> GetAssignments
            ([Optional] DateTime? availableAfter, [Optional] DateTime? availableBefore, [Optional] bool? burned, [Optional] bool? hidden, [Optional] int[] ids,
            [Optional] int[] levels, [Optional] bool? passed, [Optional] bool? resurrected, [Optional] int[] srsStages, [Optional] bool? started, [Optional] int[] subjectIds,
            [Optional] string[] subjectTypes, [Optional] bool? unlocked, [Optional] DateTime? updatedAfter)
        {
            var query = "?";

            if (availableAfter != null)
                query += "available_after=" + availableAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + And;

            if (availableBefore != null)
                query += "available_before=" + availableBefore.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + And;

            if (burned != null)
                query += "burned=" + burned + And;

            if (hidden != null)
                query += "burned=" + hidden + And;

            if (ids != null)
                query += "ids=" +string.Join(",", ids) + And;

            if (levels != null)
                query += "levels=" + string.Join(",", levels) + And;

            if (passed != null)
                query += "passed=" + passed + And;

            if (resurrected != null)
                query += "resurrected=" + resurrected + And;

            if (srsStages != null)
                query += "srs_stages=" + string.Join(",", srsStages) + And;

            if (started != null)
                query += "started=" + started + And;

            if (subjectIds != null)
                query += "subject_ids=" + string.Join(",", subjectIds) + And;

            if (subjectTypes != null)
                query += "subject_types=" + string.Join(",", subjectTypes) + And;

            if (unlocked != null)
                query += "unlocked=" + unlocked + And;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet("assignments" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json);
        }

        /// <summary>
        /// Returns a collection of all level progressions, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public static CollectionResponse<LevelProgression> GetLevelProgressions([Optional] int[] ids, [Optional] DateTime? updatedAfter)
        {
            var idsP = "";
            var updatedAfterP = "";

            if (ids != null)
                idsP = "ids=" + string.Join(",", ids);
            
            if (updatedAfter != null)
                updatedAfterP = "updatedAfter=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet($"level_progressions?{updatedAfterP}&{idsP}");
            return JsonConvert.DeserializeObject<CollectionResponse<LevelProgression>>(json);
        }
        /// <summary>
        /// Returns a collection of all resets, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public static CollectionResponse<Reset> GetResets([Optional] int[] ids, [Optional] DateTime? updatedAfter)
        {
            var query = "?";

            if (ids != null)
                query += "ids=" + string.Join(",", ids) + And;

            if (updatedAfter != null)
                query += "updated_after=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet("resets" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<Reset>>(json);
        }

        /// <summary>
        /// Returns a collection of all study material, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>        
        public static CollectionResponse<StudyMaterial> GetStudyMaterials([Optional] bool? hidden, [Optional] int[] ids, [Optional] int[] subjectIds, 
            [Optional] string[] subjectTypes, [Optional] DateTime? updatedAfter)
        {
            var query = "?";

            if (hidden != null)
                query += "hidden=" + hidden + And;

            if (ids != null)
                query += "subject_ids=" + string.Join(",", ids) + And;

            if (subjectIds != null)
                query += "subject_ids=" + string.Join(",", subjectIds) + And;

            if (subjectTypes != null)
                query += "subject_types=" + string.Join(",", subjectTypes) + And;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet("study_materials" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<StudyMaterial>>(json);
        }

        /// <summary>
        /// Returns a collection of all subjects, ordered by ascending CreatedAt, 1000 at a time.
        /// </summary>        
        public static CollectionResponse<Subject> GetSubjects([Optional] int[] ids, [Optional] string[] types, [Optional] string[] slugs, [Optional] int[] levels,
            [Optional] bool? hidden, [Optional] DateTime? updatedAfter)
        {
            var query = "?";

            if (hidden != null)
                query += "hidden=" + hidden + And;

            if (ids != null)
                query += "ids=" + string.Join(",", ids) + And;

            if (types != null)
                query += "types=" + string.Join(",", types) + And;

            if (slugs != null)
                query += "slugs=" + string.Join(",", slugs) + And;

            if (levels != null)
                query += "levels=" + string.Join(",", levels) + And;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet("subjects" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<Subject>>(json);
        }

        /// <summary>
        /// Creates a review for a specific SubjectId. Using the related AssignmentId is also a valid alternative to using SubjectId.
        /// </summary>
        public static void PostReview(Review review)
        {
            if(review.SubjectId != null && review.AssignmentId != null)
                throw new Exception("Either SubjectId or AssignmentId have to be set; not both.");

            var data = JsonConvert.SerializeObject( new { review } );
            CustomPost("reviews", data);
        }

        /// <summary>
        /// Creates a study material for a specific SubjectId. The owner of the api key can only create one StudyMaterial per SubjectId.
        /// </summary>
        public static void CreateStudyMaterial(StudyMaterial studyMaterial)
        {
            var data = JsonConvert.SerializeObject(new { study_material = studyMaterial });
            CustomPost("study_materials", data);
        }

        /// <summary>
        /// Retrieves a specific subject by its Id.
        /// </summary>        
        public static Subject GetSubject(int id)
        {
            var json = CustomGet($"subjects/{id}");
            var response = JsonConvert.DeserializeObject<ResourceResponse<object>>(json);

            switch (response.Object)
            {
                case "radical":
                    return JsonConvert.DeserializeObject<ResourceResponse<Radical>>(json).Data;
                case "kanji":
                    return JsonConvert.DeserializeObject<ResourceResponse<Kanji>>(json).Data;
                case "vocabulary":
                    return JsonConvert.DeserializeObject<ResourceResponse<Vocabulary>>(json).Data;
                default:
                    throw new Exception($"API isn't familiar with {response.Object} item type.");
            }
        }

        /// <summary>
        /// Retrieves a specific assignment by its Id.
        /// </summary>        
        public static Assignment GetAssignment(int id)
        {
            var json = CustomGet($"assignments/{id}");
            return JsonConvert.DeserializeObject<ResourceResponse<Assignment>>(json).Data;
        }

        /// <summary>
        /// Retrieves a specific study material by its Id (not SubjectId or AssignmentId).
        /// </summary>
        public static StudyMaterial GetStudyMaterial(int id)
        {
            var json = CustomGet($"study_materials/{id}");
            return JsonConvert.DeserializeObject<StudyMaterial>(json).Data;
        }

        /// <summary>
        /// Retrieves a summary report.
        /// </summary>        
        public static Summary GetSummary()
        {
            var json = CustomGet("summary");
            return JsonConvert.DeserializeObject<ResourceResponse<Summary>>(json).Data;
        }
        
        /// <summary>
        /// Returns a summary of user information.
        /// </summary>
        public static User GetUserInfo()
        {
            var json = CustomGet("user");
            return JsonConvert.DeserializeObject<ResourceResponse<User>>(json).Data;
        }
        
        /// <summary>
        /// Returns a collection of all VoiceActors, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public static CollectionResponse<VoiceActor> GetVoiceActors([Optional] int[] ids, [Optional] DateTime? updatedAfter)
        {
            var query = "?";
            
            if(ids != null)
                query +=  "ids=" + string.Join(",", ids);

            if (updatedAfter != null)
                query += "updated_after=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet($"voice_actors{query}");
            return JsonConvert.DeserializeObject<CollectionResponse<VoiceActor>>(json);
        }
        
        /// <summary>
        /// Returns a collection of all reviews, ordered by ascending CreatedAt, 1000 at a time.
        /// </summary>
        public static CollectionResponse<Review> GetReviews([Optional] int[] assignmentIds, [Optional] int[] ids, [Optional] int[] subjectIds, [Optional] DateTime? updatedAfter)
        {
            var query = "?";

            if(assignmentIds != null)
                query += "assignment_ids=" + string.Join(",", assignmentIds);

            if (ids != null)
                query += "ids=" + string.Join(",", ids);

            if (subjectIds != null)
                query += "subject_ids=" + string.Join(",", subjectIds);

            if (updatedAfter != null)
                query += "updated_after=" + updatedAfter.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var json = CustomGet($"reviews{query}");
            return JsonConvert.DeserializeObject<CollectionResponse<Review>>(json);
        }

        /// <summary>
        /// Returns assignments immediately available for review.
        /// </summary>
        public static CollectionResponse<Assignment> GetAvailableReviews()
        {
            var json = CustomGet("assignments?immediately_available_for_review");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json);
        }

        /// <summary>
        /// Returns assignments immediately available for lessons.
        /// </summary>
        public static CollectionResponse<Assignment> GetAvailableLessons()
        {
            var json = CustomGet("assignments?immediately_available_for_lessons");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json);
        }

        /// <summary>
        /// Returns assignments in review state.
        /// </summary>
        public static CollectionResponse<Assignment> GetInReview()
        {
            var json = CustomGet("assignments?in_review");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json);
        }

        /// <summary>
        /// A basic get method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2/[apiEndpointPath] Can include query parameters.</param>
        public static string CustomGet(string apiEndpointPath)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);

                var response = httpClient.GetAsync(apiEndpointPath).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// A basic post method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2/[apiEndpointPath]. Can include query parameters.</param>
        /// <param name="data">Serialized json string.</param>
        public static void CustomPost(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(apiEndpointPath, content).Result;
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// A basic put method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2/[apiEndpointPath]. Can include query parameters.</param>
        /// <param name="data">Serialized json string.</param>
        public static void CustomPut(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
                var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync(apiEndpointPath, content).Result;
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
