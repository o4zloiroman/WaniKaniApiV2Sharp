﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WanikaniApi.Models;

namespace WanikaniApi
{
    public class WaniKani
    {
        private static string apiToken;
        public static string ApiToken
        {
            get
            {
                return apiToken;
            }

            set
            {
                if (apiToken != value)
                {
                    if (!CheckApiToken(value))
                        throw new ArgumentException("Invalid API token");
                    apiToken = value;
                }
            }
        }

        /// <summary>
        /// Compares the string against the list of strings with allowed answers.
        /// </summary>        
        public static bool AnswerChecker(string answer, List<string> compareList)
        {
            answer = StringFormat(answer);
            var tollerance = DistanceTollerance(answer);

            foreach (var compare in compareList)
            {
                var distance = LevenshteinDistance.Compute(answer, compare.ToLower());
                if (tollerance >= distance)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes redundant characters from the given string. Used for comparison. 
        /// </summary>    
        public static string StringFormat(string input)
        {
            return input.ToLower().Replace("-", " ").Replace(".", "").Replace(",", "").Replace("'", "").Replace("/", "").Replace(":", "");
        }

        /// <summary>
        /// Checks the string for the ammount of allowed mistakes. 
        /// </summary>    
        public static double DistanceTollerance(string input)
        {
            switch (input.Length)
            {
                case 1:
                case 2:
                case 3:
                    return 0;
                case 4:
                case 5:
                    return 1;
                case 6:
                case 7:
                    return 2;
                default:
                    return 2 + 1 * Math.Floor((Double)(input.Length / 7));
            }
        }

        /// <summary>
        /// Bakes subjects into library specific items for review uses.
        /// </summary> 
        public static List<WaniKaniItem> BakeItems(List<ResourceResponse<Subject>> subjects)
        {
            var reviewsInProgress = new List<WaniKaniItem>();

            foreach (var subject in subjects)
            {
                var meaningsObjects = subject.Data.Meanings;
                var meanings = new List<string>();

                foreach (var meaning in meaningsObjects)
                {
                    meanings.Add(meaning.Meaning);
                }

                var readingObjects = subject.Data.Readings;
                var readings = new List<string>();

                if (subject.Object != "radical")
                {
                    foreach (var reading in readingObjects)
                    {
                        readings.Add(reading.Reading);
                    }
                }

                var review = new WaniKaniItem
                {
                    Object = subject.Object,
                    SubjectId = subject.Id,
                    Characters = subject.Data.Characters,
                    Meanings = meanings,
                    Readings = readings
                };
                reviewsInProgress.Add(review);
            }
            return reviewsInProgress;
        }

        /// <summary>
        /// The basic put method for custom requests.
        /// </summary>
        /// <param name="apiEndpointPath"></param>
        /// <param name="data">Serialized json string.</param>
        public static void Put(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiToken);
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
                StartedAt = DateTimeOffset.UtcNow
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
                    Preferences = new Models.Preferences
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

        public static void Post(string apiEndpointPath, string data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiToken);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(apiEndpointPath, content).Result;
                response.EnsureSuccessStatusCode();
            }
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
        public static List<ResourceResponse<LevelProgression>> GetAllLevelProgressions([Optional] int[] ids, [Optional] DateTimeOffset updated_after)
        {
            string idsP = "";
            string updated_afterP = "";
            var n = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero);

            if (ids != null)
                idsP = "ids=" + string.Join(",", ids);
            
            if (updated_after != n)
                updated_afterP = "updated_after=" + updated_after.ToString();

            var json = Get($"level_progressions?{updated_afterP}&{idsP}");
            var levelProgression = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<LevelProgression>>>(json).Data;
            return levelProgression;            
        }

        /// <summary>
        /// Creates reviews from a list.
        /// </summary>
        public static void CreateAReview(List<Models.Post.Review> reviews)
        {
            foreach (var reviewItem in reviews)
            {
                var createAReview = new Models.Post.CreateAReviewRoot
                {
                    Review = new Models.Post.Review
                    {
                        SubjectId = reviewItem.SubjectId,
                        IncorrectMeaningAnswers = reviewItem.IncorrectMeaningAnswers,
                        IncorrectReadingAnswers = reviewItem.IncorrectReadingAnswers
                    }
                };

                string data = JsonConvert.SerializeObject(createAReview);

                Post("reviews", data);
            }           
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

        public static string Get(string apiEndpointPath)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiToken);
                var response = httpClient.GetAsync(apiEndpointPath).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return json;
            }
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
        /// Returns assignments which are immediately available for lessons.
        /// </summary>
        public static List<ResourceResponse<Subject>> GetSubjectsAvailableForReview()
        {
            var summary = GetSummary();
            if (summary.Reviews[0].SubjectIds.Length != 0)
            {
                var json = Get("assignments?immediately_available_for_review");
                var assignments = JsonConvert.DeserializeObject<CollectionResponse<ResourceResponse<Assignments>>>(json).Data;
                return AssignmentsIntoSubjects(assignments);
            }
            else
                throw new Exception("No reviews available.");
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
                throw new Exception("No lessons available.");
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
                throw new Exception("No subjects are in review state.");
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

        private static readonly Regex _apiTokenRegex = new Regex("^[a-f0-9-]{36}$");
        private static bool CheckApiToken(string apiKey)
        {
            return _apiTokenRegex.IsMatch(apiKey);
        }
    }
}