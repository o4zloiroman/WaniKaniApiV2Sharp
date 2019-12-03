using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WanikaniApi.Models;

namespace WanikaniApi
{
    public class WaniKaniClient
    {
        private static string _apiToken;
        private static readonly Regex _apiRegex = new Regex("^[a-f0-9-]{36}$");

        public static string ApiToken
        {
            get
            {
                return _apiToken;
            }
            set
            {
                if (_apiRegex.IsMatch(value))
                    _apiToken = ApiToken;
                else
                {
                    throw new ArgumentException("Invalid API token.");
                }
            }
        }

        public WaniKaniClient(string apiToken)
        {
            var apiRegex = new Regex("^[a-f0-9-]{36}$");
            if(apiRegex.IsMatch(apiToken))
                _apiToken = apiToken;
            else
            {
                throw new ArgumentException("Invalid API token.");
            }
        }

        /// <summary>
        /// A basic get method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2/[apiEndpointPath] Can include query parameters.</param>
        public static string Get(string apiEndpointPath)
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
        public static void Post(string apiEndpointPath, string data)
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
        public static void Put(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PutAsync(apiEndpointPath, content).Result;
                response.EnsureSuccessStatusCode();
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

            Put($"assignments/{id}/start", data);
        }

        /// <summary>
        /// Returns an updated summary of user information.
        /// </summary>
        public static void UpdateUserInformation
            (int lessonsBatchSize, bool lessonsAutoplayAudio, bool reviewsAutoplayAudio, bool reviewsDisplaySrsIndicator, string lessonsPresentationOrder)
        {
            var updateUserInformation = new
            {
                user = new
                {
                    Preferences = new Preferences
                    {
                        LessonsBatchSize = lessonsBatchSize,
                        LessonsAutoplayAudio = lessonsAutoplayAudio,
                        ReviewsAutoplayAudio = reviewsAutoplayAudio,
                        ReviewsDisplaySrsIndicator = reviewsDisplaySrsIndicator,
                        LessonsPresentationOrder = lessonsPresentationOrder
                    }
                }
            };

            var data = JsonConvert.SerializeObject(updateUserInformation);

            Put("user", data);
        }

        /// <summary>
        /// Updates a study material for a specific id.
        /// </summary>
        public static void UpdateStudyMaterial(int subjectId, string meaningNote = null, string readingNote = null, List<string> meaningSynonyms = null)
        {
            var createAStudyMaterial = new
            {
                study_material = new
                {
                    subject_id = subjectId,
                    meaning_note = meaningNote,
                    reading_note = readingNote,
                    meaning_synonyms = meaningSynonyms
                }
            };

            var data = JsonConvert.SerializeObject(createAStudyMaterial);

            Put("study_materials", data);
        }

        /// <summary>
        /// Returns a collection of all assignments, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        public static List<Assignment> GetAssignments
            ([Optional] DateTime? availableAfter, [Optional] DateTime? availableBefore, [Optional] bool? burned, [Optional] bool? hidden, [Optional] int[] ids,
            [Optional] int[] levels, [Optional] bool? passed, [Optional] bool? resurrected, [Optional] int[] srsStages, [Optional] bool? started, [Optional] int[] subjectIds,
            [Optional] string[] subjectTypes, [Optional] bool? unlocked, [Optional] DateTime? updatedAfter)
        {
            const string and = "&";
            var query = "?";

            if (availableAfter != null)
                query += "available_after=" + availableAfter.Value.ToUniversalTime() + and;

            if (availableBefore != null)
                query += "available_before=" + availableBefore.Value.ToUniversalTime() + and;

            if (burned != null)
                query += "burned=" + burned + and;

            if (hidden != null)
                query += "burned=" + hidden + and;

            if (ids != null)
                query += "ids=" +string.Join(",", ids) + and;

            if (levels != null)
                query += "levels=" + string.Join(",", levels) + and;

            if (passed != null)
                query += "passed=" + passed + and;

            if (resurrected != null)
                query += "resurrected=" + resurrected + and;

            if (srsStages != null)
                query += "srs_stages=" + string.Join(",", srsStages) + and;

            if (started != null)
                query += "started=" + started + and;

            if (subjectIds != null)
                query += "subject_ids=" + string.Join(",", subjectIds) + and;

            if (subjectTypes != null)
                query += "subject_types=" + string.Join(",", subjectTypes) + and;

            if (unlocked != null)
                query += "unlocked=" + unlocked + and;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime();

            var json = Get("assignments" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json).Data;
        }

        /// <summary>
        /// Returns a collection of all level progressions, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>
        /// <param name="ids">Only LevelProgressions where Data.Id matches one of the array values are returned.</param>
        /// <param name="updatedAfter">Only LevelProgressions updated after this time are returned.</param>
        /// <returns></returns>
        public static List<LevelProgression> GetLevelProgressions([Optional] int[] ids, [Optional] DateTime? updatedAfter)
        {
            var idsP = "";
            var updatedAfterP = "";

            if (ids != null)
                idsP = "ids=" + string.Join(",", ids);
            
            if (updatedAfter != null)
                updatedAfterP = "updatedAfter=" + updatedAfter.Value.ToUniversalTime();

            var json = Get($"level_progressions?{updatedAfterP}&{idsP}");
            return JsonConvert.DeserializeObject<CollectionResponse<LevelProgression>>(json).Data;
        }

        /// <summary>
        /// Returns a collection of all study material, ordered by ascending CreatedAt, 500 at a time.
        /// </summary>        
        public static List<StudyMaterial> GetStudyMaterials([Optional] bool? hidden, [Optional] int[] ids, [Optional] int[] subjectIds, 
            [Optional] string[] subjectTypes, [Optional] DateTime? updatedAfter)
        {
            const string and = "&";
            var query = "?";

            if (hidden != null)
                query += "hidden=" + hidden + and;

            if (ids != null)
                query += "subject_ids=" + string.Join(",", ids) + and;

            if (subjectIds != null)
                query += "subject_ids=" + string.Join(",", subjectIds) + and;

            if (subjectTypes != null)
                query += "subject_types=" + string.Join(",", subjectTypes) + and;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime();

            var json = Get("study_materials" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<StudyMaterial>>(json).Data;
        }

        /// <summary>
        /// Returns a collection of all subjects, ordered by ascending CreatedAt, 1000 at a time.
        /// </summary>        
        public static List<ISubject> GetSubjects([Optional] int[] ids, [Optional] string[] types, [Optional] string[] slugs, [Optional] int[] levels,
            [Optional] bool? hidden, [Optional] DateTime? updatedAfter)
        {
            var query = "?";
            const string and = "&";

            if (hidden != null)
                query += "hidden=" + hidden + and;

            if (ids != null)
                query += "ids=" + string.Join(",", ids) + and;

            if (types != null)
                query += "types=" + string.Join(",", types) + and;

            if (slugs != null)
                query += "slugs=" + string.Join(",", slugs) + and;

            if (levels != null)
                query += "levels=" + string.Join(",", levels) + and;

            if (updatedAfter != null)
                query += "updatedAfter=" + updatedAfter.Value.ToUniversalTime();

            var json = Get("subjects" + query.ToLower());
            return JsonConvert.DeserializeObject<CollectionResponse<ISubject>>(json).Data;
        }

        /// <summary>
        /// Creates a review for a specific subjectId. Using the related assignmentId is also a valid alternative to using subjectId.
        /// </summary>
        public static void PostReview(int subjectId, [Optional] int? assignmentId, int incorrectMeaningAnswers, int incorrectReadingAnswers)
        {
            object review;

            if (assignmentId != null)
            {
                review = new
                {
                    review = new
                    {
                        assignment_id = assignmentId,
                        incorrect_meaning_answers = incorrectMeaningAnswers,
                        incorrect_reading_answers = incorrectReadingAnswers
                    }
                };
            }
            else
            {
                review = new
                {
                    review = new
                    {
                        subject_id = subjectId,
                        incorrect_meaning_answers = incorrectMeaningAnswers,
                        incorrect_reading_answers = incorrectReadingAnswers
                    }
                };
            }

            var data = JsonConvert.SerializeObject(review);
            Post("reviews", data);
        }

        /// <summary>
        /// Creates a study material for a specific subject_id. The owner of the api key can only create one study_material per subject_id.
        /// </summary>
        public static void PostStudyMaterial(int subjectId, string meaningNote, string readingNote, List<string> meaningSynonyms)
        {
            var createAStudyMaterial = new
            {
                study_material = new
                {
                    subject_id = subjectId,
                    meaning_note = meaningNote,
                    reading_note = readingNote,
                    meaning_synonyms = meaningSynonyms
                }
            };

            var data = JsonConvert.SerializeObject(createAStudyMaterial);

            Post("study_materials", data);
        }

        /// <summary>
        /// Retrieves a specific subject by its id.
        /// </summary>        
        public static Subject GetSubject(int id)
        {
            var json = Get($"subjects/{id}");
            return JsonConvert.DeserializeObject<Subject>(json).Data;
        }

        /// <summary>
        /// Retrieves a specific assignment by its id.
        /// </summary>        
        public Assignment GetAssignment(int id)
        {
            var json = Get($"assignments/{id}");
            return JsonConvert.DeserializeObject<Assignment>(json).Data;
        }

        /// <summary>
        /// Retrieves a specific study material by its id (not subject_id or assignment_id).
        /// </summary>
        public static StudyMaterial GetStudyMaterial(int id)
        {
            var json = Get($"study_materials/{id}");
            return JsonConvert.DeserializeObject<StudyMaterial>(json).Data;
        }

        /// <summary>
        /// Retrieves a summary report.
        /// </summary>        
        public static Summary GetSummary()
        {
            var json = Get("summary");
            return JsonConvert.DeserializeObject<Summary>(json).Data;
        }
        
        /// <summary>
        /// Returns a summary of user information.
        /// </summary>
        public static User GetUserInfo()
        {
            var json = Get("user");
            return JsonConvert.DeserializeObject<User>(json).Data;
        }

        public static List<VoiceActor> GetVoiceActors([Optional] int[] ids, [Optional] DateTime? updatedAfter)
        {
            var idsP = "";
            var updatedAfterP = "";

            if(ids != null)
                idsP = string.Join(",", ids);

            if (updatedAfter != null)
                updatedAfterP = updatedAfter.Value.ToUniversalTime().ToString();

            var json = Get($"voice_actors?ids={idsP}&updated_after={updatedAfterP}");
            return JsonConvert.DeserializeObject<CollectionResponse<VoiceActor>>(json).Data;
        }

        /// <summary>
        /// Returns assignments which are immediately available for review.
        /// </summary>
        public static List<Assignment> GetReviews()
        {
            var summary = GetSummary();
            if (summary.Reviews[0].SubjectIds.Length == 0) return null;

            var json = Get("assignments?immediately_available_for_review");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json).Data;
        }

        /// <summary>
        /// Returns assignments which are immediately available for lessons.
        /// </summary>
        public static List<Assignment> GetLessons()
        {
            var summary = GetSummary();
            if (summary.Lessons[0].SubjectIds.Length == 0) return null;

            var json = Get("assignments?immediately_available_for_lessons");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json).Data;
        }

        /// <summary>
        /// Returns assignments which are in the review state.
        /// </summary>
        public static List<Assignment> GetInReview()
        {
            var summary = GetSummary();
            if (summary.Lessons[0].SubjectIds.Length == 0) return null;

            var json = Get("assignments?in_review");
            return JsonConvert.DeserializeObject<CollectionResponse<Assignment>>(json).Data;
        }
    }
}
