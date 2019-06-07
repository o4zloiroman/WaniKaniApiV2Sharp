using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WanikaniApi.Models;

namespace WanikaniApi
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class WaniKani
    {
        private static string _apiToken;
        private static readonly Regex ApiTokenRegex = new Regex("^[a-f0-9-]{36}$");
        private static bool CheckApiToken(string apiKey)
        {
            return ApiTokenRegex.IsMatch(apiKey);
        }
        public static string ApiToken
        {
            get => _apiToken;

            set
            {
                if (_apiToken != value)
                {
                    if (!CheckApiToken(value))
                        throw new ArgumentException("Invalid API token");
                    _apiToken = value;
                }
            }
        }

        /// <summary>
        /// A basic get method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2[apiEndpointPath]. Can include query parameters.</param>
        /// <returns></returns>
        public static string Get(string apiEndpointPath)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
                var response = httpClient.GetAsync(apiEndpointPath).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return json;
            }
        }

        /// <summary>
        /// A basic post method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2[apiEndpointPath]. Can include query parameters.</param>
        /// <param name="data">Serialized json string.</param>
        public static void Post(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(apiEndpointPath, content).Result;
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// A basic put method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath">https://api.wanikani.com/v2[apiEndpointPath]. Can include query parameters.</param>
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
        public static void StartAnAssignment(int id)
        {
            var startAssignment = new Models.Put.StartAnAssignmentRoot
            {
                StartedAt = DateTime.UtcNow
            };

            string data = JsonConvert.SerializeObject(startAssignment);

            Put("assignments/" + id + "/start", data);
        }

        /// <summary>
        /// Returns an updated summary of user information.
        /// </summary>
        public static void UpdateUserInformation
            (int LessonsBatchSize, bool LessonsAutoplayAudio, bool ReviewsAutoplayAudio, bool ReviewsDisplaySrsIndicator, string LessonsPresentationOrder)
        {
            var updateUserInformation = new Models.Put.UpdateUserInformationRoot
            {
                User = new Models.Put.User
                {
                    Preferences = new Preferences
                    {
                        LessonsBatchSize = LessonsBatchSize,
                        LessonsAutoplayAudio = LessonsAutoplayAudio,
                        ReviewsAutoplayAudio = ReviewsAutoplayAudio,
                        ReviewsDisplaySrsIndicator = ReviewsDisplaySrsIndicator,
                        LessonsPresentationOrder = LessonsPresentationOrder
                    }
                }
            };

            string data = JsonConvert.SerializeObject(updateUserInformation);

            Put("user", data);
        }

        /// <summary>
        /// Updates a study material for a specific id.
        /// </summary>
        public static void UpdateAStudyMaterial(int SubjectId, string MeaningNote = null, string ReadingNote = null, List<string> MeaningSynonyms = null)
        {
            var createAStudyMaterial = new Models.Post.CreateAStudyRoot
            {
                StudyMaterial = new Models.Post.StudyMaterial
                {
                    SubjectId = SubjectId,
                    MeaningNote = MeaningNote,
                    ReadingNote = ReadingNote,
                    MeaningSynonyms = MeaningSynonyms
                }
            };

            string data = JsonConvert.SerializeObject(createAStudyMaterial);

            Put("study_materials", data);
        }

        /// <summary>
        /// Returns a collection of all assignments, ordered by ascending created_at, 500 at a time.
        /// </summary>
        /// <param name="available_after">Only assignments available at or after this time are returned.</param>
        /// <param name="available_before">Only assignments available at or before this time are returned.</param>
        /// <param name="burned">When set to true, returns assignments that have a value in data.burned_at. Returns assignments with a null data.burned_at if false.</param>
        /// <param name="hidden">Return assignments with a matching value in the hidden attribute</param>
        /// <param name="ids">Only assignments where data.id matches one of the array values are returned.</param>
        /// <param name="levels">Only assignments where the associated subject level matches one of the array values are returned. Valid values range from 1 to 60.</param>
        /// <param name="passed">Returns assignments where data.passed equals passed.</param>
        /// <param name="resurrected">Returns assignments where data.resurrected equals resurrected.</param>
        /// <param name="srs_stages">Only assignments where data.srs_stage matches one of the array values are returned. Valid values range from 0 to 9</param>
        /// <param name="started">When set to true, returns assignments that have a value in data.started_at. Returns assignments with a null data.started_at if false.</param>
        /// <param name="subject_ids">Only assignments where data.subject_id matches one of the array values are returned.</param>
        /// <param name="subject_types">Only assignments where data.subject_type matches one of the array values are returned. Valid values are: radical, kanji, or vocabulary.</param>
        /// <param name="unlocked">When set to true, returns assignments that have a value in data.unlocked_at. Returns assignments with a null data.unlocked_at if false.</param>
        /// <param name="updated_after">Only assignments updated after this time are returned.</param>
        /// <returns></returns>
        public static List<ResourceResponse<Assignments>> GetAllAssignments
            ([Optional] DateTime? available_after, [Optional] DateTime? available_before, [Optional] bool? burned, [Optional] bool? hidden, [Optional] int[] ids,
            [Optional] int[] levels, [Optional] bool? passed, [Optional] bool? resurrected, [Optional] int[] srs_stages, [Optional] bool? started, [Optional] int[] subject_ids,
            [Optional] string[] subject_types, [Optional] bool? unlocked, [Optional] DateTime? updated_after)
        {
            string query = "?";
            string and = "&";

            if (available_after != null)
                query += "available_after=" + available_after.ToString() + and;

            if (available_before != null)
                query += "available_before=" + available_before.ToString() + and;

            if (burned != null)
                query += "burned=" + burned + and;

            if (hidden != null)
                query += "burned=" + burned + and;

            if (ids != null)
                query += "ids=" +string.Join(",", ids) + and;

            if (levels != null)
                query += "levels=" + string.Join(",", levels) + and;

            if (resurrected != null)
                query += "resurrected=" + resurrected + and;

            if (srs_stages != null)
                query += "srs_stages=" + string.Join(",", srs_stages) + and;

            if (started != null)
                query += "started=" + started + and;

            if (subject_ids != null)
                query += "subject_ids=" + string.Join(",", subject_ids) + and;

            if (subject_types != null)
                query += "subject_types=" + string.Join(",", subject_types) + and;

            if (unlocked != null)
                query += "unlocked=" + unlocked + and;

            if (updated_after != null)
                query += "updated_after=" + updated_after.ToString();

            var json = Get("assignments" + query.ToLower());
            var assignments  = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Assignments>>>(json).Data;
            return assignments;
        }

        /// <summary>
        /// Returns a collection of all level progressions, ordered by ascending created_at, 500 at a time.
        /// </summary>
        /// <param name="ids">Only level progressions where data.id matches one of the array values are returned.</param>
        /// <param name="updated_after">Only level_progressions updated after this time are returned.</param>
        /// <returns></returns>
        public static List<ResourceResponse<LevelProgression>> GetAllLevelProgressions([Optional] int[] ids, [Optional] DateTime updated_after)
        {
            string idsP = "";
            string updated_afterP = "";
            var n = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero);

            if (ids != null)
                idsP = "ids=" + string.Join(",", ids);
            
            if (updated_after != n)
                updated_afterP = "updated_after=" + updated_after;

            var json = Get($"level_progressions?{updated_afterP}&{idsP}");
            var levelProgression = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<LevelProgression>>>(json).Data;
            return levelProgression;            
        }

        /// <summary>
        /// Returns a collection of all study material, ordered by ascending created_at, 500 at a time.
        /// </summary>        
        public static List<ResourceResponse<StudyMaterial>> GetAllStudyMaterials([Optional] bool? hidden, [Optional] int[] ids, [Optional] int[] subject_ids, 
            [Optional] string[] subject_types, [Optional] DateTime? updated_after)
        {
            string query = "?";
            string and = "&";

            if (hidden != null)
                query += "hidden=" + hidden + and;

            if (ids != null)
                query += "subject_ids=" + string.Join(",", ids) + and;

            if (subject_ids != null)
                query += "subject_ids=" + string.Join(",", subject_ids) + and;

            if (subject_types != null)
                query += "subject_types=" + string.Join(",", subject_types) + and;

            if (updated_after != null)
                query += "updated_after=" + updated_after.ToString();

            var json = Get("study_materials" + query.ToLower());
            var studyMaterials = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<StudyMaterial>>>(json).Data;
            return studyMaterials;
        }

        /// <summary>
        /// Returns a collection of all subjects, ordered by ascending created_at, 1000 at a time.
        /// </summary>        
        public static List<ResourceResponse<Subject>> GetAllSubjects([Optional] int[] ids, [Optional] string[] types, [Optional] string[] slugs, [Optional] int[] levels,
            [Optional] bool? hidden, [Optional] DateTime? updated_after)
        {
            string query = "?";
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

            if (updated_after != null)
                query += "updated_after=" + updated_after;

            var json = Get("subjects" + query.ToLower());
            var subjects = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Subject>>>(json).Data;
            return subjects;
        }

        /// <summary>
        /// Creates a review for a specific subject_id. Using the related assignment_id is also a valid alternative to using subject_id. 
        /// Either of those have to bet set, but not both.
        /// </summary>
        public static void CreateAReview(int? subject_id, int? assignment_id, int incorrect_meaning_answers, int incorrect_reading_answers)
        {
            Models.Post.CreateAReviewRoot review;
            if (subject_id != null)
            {
                review = new Models.Post.CreateAReviewRoot
                {
                    Review = new Models.Post.Review
                    {
                        SubjectId = subject_id,
                        IncorrectMeaningAnswers = incorrect_meaning_answers,
                        IncorrectReadingAnswers = incorrect_reading_answers
                    }
                };
            }
            else if(assignment_id != null)
            {
                review = new Models.Post.CreateAReviewRoot
                {
                    Review = new Models.Post.Review
                    {
                        AssignmentId = assignment_id,
                        IncorrectMeaningAnswers = incorrect_meaning_answers,
                        IncorrectReadingAnswers = incorrect_reading_answers
                    }
                };
            }
            else
            {
                throw new Exception("Either assignment_id or subject_id have to be set, but not both.");
            }

            string data = JsonConvert.SerializeObject(review);

            Post("reviews", data);
        }

        /// <summary>
        /// Creates a study material for a specific subject_id. The owner of the api key can only create one study_material per subject_id.
        /// </summary>
        public static void CreateAStudyMaterial(int SubjectId, string MeaningNote, string ReadingNote, List<string> MeaningSynonyms)
        {
            var createAStudyMaterial = new Models.Post.CreateAStudyRoot
            {
                StudyMaterial = new Models.Post.StudyMaterial
                {
                    SubjectId = SubjectId,
                    MeaningNote = MeaningNote,
                    ReadingNote = ReadingNote,
                    MeaningSynonyms = MeaningSynonyms
                }
            };

            string data = JsonConvert.SerializeObject(createAStudyMaterial);

            Post("study_materials", data);
        }

        /// <summary>
        /// Retrieves a specific subject by its id.
        /// </summary>        
        public static Subject GetASpecificSubject(int id)
        {
            var json = Get($"subjects/{id}");
            var subject = JsonConvert.DeserializeObject<ResourceResponse<Subject>>(json).Data;
            return subject;
        }

        /// <summary>
        /// Retrieves a specific assignment by its id.
        /// </summary>        
        public static Assignments GetASpecificAssignment(int id)
        {
            var json = Get($"assignments/{id}");
            var assignment = JsonConvert.DeserializeObject<ResourceResponse<Assignments>>(json).Data;
            return assignment;
        }

        /// <summary>
        /// Retrieves a specific study material by its id (not subject_id or assignment_id).
        /// </summary>
        public static StudyMaterial GetASpecificStudyMaterial(int id)
        {
            var json = Get($"study_materials/{id}");
            var studyMaterial = JsonConvert.DeserializeObject<ResourceResponse<StudyMaterial>>(json).Data;
            return studyMaterial;
        }

        /// <summary>
        /// Retrieves a summary report.
        /// </summary>        
        public static Summary GetSummary()
        {
            var json = Get("summary");
            var summary = JsonConvert.DeserializeObject<BaseResponse<Summary>>(json).Data;
            return summary;
        }
        
        /// <summary>
        /// Returns a summary of user information.
        /// </summary>
        public static User GetUserInfo()
        {
            var json = Get("user");
            var user = JsonConvert.DeserializeObject<BaseResponse<User>>(json).Data;
            return user;
        }

        /// <summary>
        /// Retrieves a specific study material related to a specific subject.
        /// </summary>        
        public static StudyMaterial GetAStudyMaterial(int subject_id)
        {
            var json = Get($"study_materials/?subject_ids={subject_id}");
            try
            {
                var studyMaterial = JsonConvert.DeserializeObject<ResourceResponse<StudyMaterial>>(json).Data;
                return studyMaterial;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns assignments which are immediately available for lessons.
        /// </summary>
        public static List<ResourceResponse<Subject>> GetSubjectsAvailableForReview()
        {
            var summary = GetSummary();
            if (summary.Reviews[0].SubjectIds.Length == 0) return null;

            var json = Get("assignments?immediately_available_for_review");
            var assignments = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Assignments>>>(json).Data;
            return AssignmentsIntoSubjects(assignments);

        }

        /// <summary>
        /// Returns assignments which are immediately available for review.
        /// </summary>
        public static List<ResourceResponse<Subject>> GetSubjectsAvailableForLessons()
        {
            var summary = GetSummary();
            if (summary.Lessons[0].SubjectIds.Length != 0)
            {
                var json = Get("assignments?immediately_available_for_lessons");
                var assignments = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Assignments>>>(json).Data;
                return AssignmentsIntoSubjects(assignments);
            }
            else
                return null;
        }

        /// <summary>
        /// Returns assignments which are in the review state.
        /// </summary>
        public static List<ResourceResponse<Subject>> GetSubjectsInReview()
        {
            var summary = GetSummary();
            if (summary.Lessons[0].SubjectIds.Length != 0)
            {
                var json = Get("assignments?in_review");
                var assignments = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Assignments>>>(json).Data;
                return AssignmentsIntoSubjects(assignments);
            }
            else
                throw new Exception("No subjects are in the review state.");
        }

        /// <summary>
        /// Takes a list of assignments and returns a list of subjects.
        /// </summary>
        public static List<ResourceResponse<Subject>> AssignmentsIntoSubjects(List<ResourceResponse<Assignments>> assignments)
        {
            var ids = new List<long>();

            foreach (var assignment in assignments)
            {
                ids.Add(assignment.Data.SubjectId);
            }

            var json = Get("subjects?ids=" + String.Join(",", ids));
            var subjects = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Subject>>>(json).Data;
            return subjects;
        }
    }
}
