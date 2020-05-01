using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WaniKaniApi.Extensions;
using WaniKaniApi.Models;
using WaniKaniApi.Singleton;

namespace WaniKaniApi
{
    public class ApiClient
    {
        private readonly Regex ApiRegex = new Regex("^[a-f0-9-]{36}$");

        public ApiClient(string apiToken)
        {
            if (!ApiRegex.IsMatch(apiToken))
            {
                throw new ArgumentException("Wrong token format.");
            }

            HttpClientSingleton.GetInstance(apiToken);
        }
        
        /// <summary>
        /// Mark the assignment as started, moving the assignment from the lessons to the review queue. 
        /// </summary>
        /// <param name="id">Unique identifier of the assignment.</param>
        public async Task StartAssignment(int id)
        {
            var startAssignment = new
            {
                started_at = DateTime.UtcNow
            };

            var data = JsonConvert.SerializeObject(startAssignment);

            await HttpClientSingleton.CustomPut($"assignments/{id}/start", data);
        }

        /// <summary>
        /// Returns an updated summary of user information.
        /// </summary>
        public async Task UpdateUserInformation (Preferences preferences)
        {
            var updateUserInformation = new
            {
                user = new
                {
                    preferences
                }
            };

            var data = JsonConvert.SerializeObject(updateUserInformation);

            await HttpClientSingleton.CustomPut("user", data);
        }

        /// <summary>
        /// Updates a study material for a specific Id.
        /// </summary>
        public async Task UpdateStudyMaterial(StudyMaterial studyMaterial)
        {
            var createAStudyMaterial = new
            {
                study_material = studyMaterial
            };

            var data = JsonConvert.SerializeObject(createAStudyMaterial);

            await HttpClientSingleton.CustomPut("study_materials", data);
        }

        /// <summary>
        /// Returns a collection of all assignments, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public async Task<CollectionResponse<Assignment>> GetAssignments
            ([Optional] DateTime? availableAfter, [Optional] DateTime? availableBefore, [Optional] bool? burned, 
            [Optional] bool? hidden, [Optional] IEnumerable<int> ids, [Optional] IEnumerable<int> levels, 
            [Optional] bool? passed, [Optional] bool? resurrected, [Optional] IEnumerable<SrsStageEnum> srsStages, 
            [Optional] bool? started, [Optional] IEnumerable<int> subjectIds, [Optional] IEnumerable<string> subjectTypes, 
            [Optional] bool? unlocked, [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "available_after", availableAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "available_before", availableBefore?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "burned", burned.ToString() },
                { "hidden", hidden.ToString() },
                { "ids", ids.Stringify() },
                { "levels", levels.Stringify() },
                { "passed", passed.ToString() },
                { "resurrected", resurrected.ToString() },
                { "srs_stages", srsStages.Stringify() },
                { "started", started.ToString() },
                { "subject_ids", subjectIds.Stringify() },
                { "subject_types", subjectTypes.Stringify() },
                { "unlocked", unlocked.ToString() },
                { "updatedAfter", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };

            return await HttpClientSingleton.CustomGet<CollectionResponse<Assignment>>("assignments", parameters);
        }

        /// <summary>
        /// Returns a collection of all level progressions, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public async Task<CollectionResponse<LevelProgression>> GetLevelProgressions([Optional] IEnumerable<int> ids, 
            [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ids", ids.Stringify() },
                { "updatedAfter", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };
            
            return await HttpClientSingleton.CustomGet<CollectionResponse<LevelProgression>>("level_progressions", parameters);
        }
        /// <summary>
        /// Returns a collection of all resets, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public async Task<CollectionResponse<Reset>> GetResets([Optional] IEnumerable<int> ids, [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ids", ids.Stringify() },
                { "updatedAfter", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };
            
            return await HttpClientSingleton.CustomGet<CollectionResponse<Reset>>("resets", parameters);
        }

        /// <summary>
        /// Returns a collection of all study material, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>        
        public async Task<CollectionResponse<StudyMaterial>> GetStudyMaterials([Optional] bool? hidden, 
            [Optional] IEnumerable<int> ids, [Optional] IEnumerable<int> subjectIds, 
            [Optional] IEnumerable<string> subjectTypes, [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "hidden", hidden.ToString() },
                { "ids", ids.Stringify() },
                { "subject_ids", subjectIds.Stringify() },
                { "subject_types", subjectTypes.Stringify() },
                { "updatedAfter", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };

            return await HttpClientSingleton.CustomGet<CollectionResponse<StudyMaterial>>("study_materials", parameters);
        }

        /// <summary>
        /// Returns a collection of all subjects, ordered by ascending CreatedAt, 1000 at a time.
        /// </summary>        
        public async Task<CollectionResponse<Subject>> GetSubjects([Optional] IEnumerable<int> ids, 
            [Optional] IEnumerable<string> types, [Optional] IEnumerable<string> slugs, [Optional] IEnumerable<int> levels,
            [Optional] bool? hidden, [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "hidden", hidden.ToString() },
                { "ids", ids.Stringify() },
                { "types", types.Stringify() },
                { "slugs", slugs.Stringify() },
                { "levels", levels.Stringify() },
                { "updatedAfter", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };

            return await HttpClientSingleton.CustomGet<CollectionResponse<Subject>>("subjects", parameters);
        }

        /// <summary>
        /// Creates a review for a specific SubjectId. Using the related AssignmentId is also a valid alternative to using SubjectId.
        /// </summary>
        public async Task CreateReview(Review review)
        {
            if(review.SubjectId != null && review.AssignmentId != null)
                throw new Exception("Either SubjectId or AssignmentId has to be set; not both.");

            var data = JsonConvert.SerializeObject( new { review } );
            await HttpClientSingleton.CustomPost("reviews", data);
        }

        /// <summary>
        /// Creates a study material for a specific SubjectId. The owner of the api key can only create one StudyMaterial per SubjectId.
        /// </summary>
        public async Task CreateStudyMaterial(StudyMaterial studyMaterial)
        {
            var data = JsonConvert.SerializeObject(new { study_material = studyMaterial });
            await HttpClientSingleton.CustomPost("study_materials", data);
        }

        /// <summary>
        /// Retrieves a specific subject by its Id.
        /// </summary>        
        public async Task<Subject> GetSubject(int id)
        {
            var response = await HttpClientSingleton.CustomGet<ResourceResponse<JObject>>($"subjects/{id}");
            var jObject = response.Data;

            return response.Object switch
            {
                "radical" => jObject.ToObject<Radical>(),
                "kanji" => jObject.ToObject<Kanji>(),
                "vocabulary" => jObject.ToObject<Vocabulary>(),
                _ => throw new Exception($"API doesn't support the {response.Object} item type.")
            };
        }

        /// <summary>
        /// Retrieves a specific assignment by its Id.
        /// </summary>        
        public async Task<Assignment> GetAssignment(int id)
        {
            var response = await HttpClientSingleton.CustomGet<ResourceResponse<Assignment>>($"assignments/{id}");
            return response.Data;
        }

        /// <summary>
        /// Retrieves a specific study material by its Id (not SubjectId or AssignmentId).
        /// </summary>
        public async Task<StudyMaterial> GetStudyMaterial(int id)
        {
            var response = await HttpClientSingleton.CustomGet<StudyMaterial>($"study_materials/{id}");
            return response.Data;
        }

        /// <summary>
        /// Retrieves a summary report.
        /// </summary>        
        public async Task<Summary> GetSummary()
        {
            var response = await HttpClientSingleton.CustomGet<BaseResponse<Summary>>("summary");
            return response.Data;
        }
        
        /// <summary>
        /// Returns a summary of user information.
        /// </summary>
        public async Task<User> GetUserInfo()
        {
            var response = await HttpClientSingleton.CustomGet<BaseResponse<User>>("user");
            return response.Data;
        }
        
        /// <summary>
        /// Returns a collection of all VoiceActors, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public async Task<CollectionResponse<VoiceActor>> GetVoiceActors([Optional] IEnumerable<int> ids, 
            [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "ids", ids.Stringify() },
                { "updated_after", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };

            return await HttpClientSingleton.CustomGet<CollectionResponse<VoiceActor>>("voice_actors", parameters);
        }
        
        /// <summary>
        /// Returns a collection of all reviews, ordered by ascending CreatedAt, 1000 at a time.
        /// </summary>
        public async Task<CollectionResponse<Review>> GetReviews([Optional] IEnumerable<int> assignmentIds, 
            [Optional] IEnumerable<int> ids, [Optional] IEnumerable<int> subjectIds, [Optional] DateTime? updatedAfter)
        {
            var parameters = new Dictionary<string, string>
            {
                { "assignment_ids", assignmentIds.Stringify() },
                { "ids", ids.Stringify() },
                { "subject_ids", subjectIds.Stringify() },
                { "updated_after", updatedAfter?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            };

            return await HttpClientSingleton.CustomGet<CollectionResponse<Review>>("reviews", parameters);
        }

        /// <summary>
        /// Returns assignments immediately available for review.
        /// </summary>
        public async Task<CollectionResponse<Assignment>> GetAvailableReviews()
        {
            return await HttpClientSingleton.CustomGet<CollectionResponse<Assignment>>("assignments?immediately_available_for_review");
        }

        /// <summary>
        /// Returns assignments immediately available for lessons.
        /// </summary>
        public async Task<CollectionResponse<Assignment>> GetAvailableLessons()
        {
            return await HttpClientSingleton.CustomGet<CollectionResponse<Assignment>>("assignments?immediately_available_for_lessons");
        }

        /// <summary>
        /// Returns assignments in review state.
        /// </summary>
        public async Task<CollectionResponse<Assignment>> GetInReview()
        {
            return await HttpClientSingleton.CustomGet<CollectionResponse<Assignment>>("assignments?in_review");
        }
    }
}
