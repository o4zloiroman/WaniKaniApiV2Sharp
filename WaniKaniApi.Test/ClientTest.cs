using System.Threading.Tasks;
using Xunit;

namespace WaniKaniApi.Tests
{
    public class ClientTest
    {
        public ClientTest()
        {
            
        }

        
        // https://www.wanikani.com/settings/personal_access_tokens
        [Theory]
        [InlineData("API_KEY")]
        public async Task Test1(string apiToken)
        {
            var apiClient = new ApiClient(apiToken);
            
            var userInfo = await apiClient.GetUserInfo();
            var preferences = userInfo.Preferences;
            await apiClient.UpdateUserInformation(preferences);
            
            var summary = await apiClient.GetSummary();
            var availableForReview = await apiClient.GetAvailableReviews();
            var availableForLessons = await apiClient.GetAvailableLessons();
            var filter = await apiClient.GetSubjects();
            
            var assignments = await apiClient.GetAssignments();
            var subject = await apiClient.GetSubject(199);
        }
    }
}