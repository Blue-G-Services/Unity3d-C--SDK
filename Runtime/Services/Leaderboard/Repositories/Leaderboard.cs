using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Leaderboard.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Leaderboard.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {

        public Task<WebRequest.ResponseWrapper<RowListResponse<Models.Leaderboard>>> GetLeaderBoards<T>(T input) where T : GetLeaderboardsParams
        {

            return WebRequest.Get<RowListResponse<Models.Leaderboard>>(
                UrlMap.GetLeaderboardsUrl(input.skip, input.limit, input.label));
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<TOutput>>> GetPartiesScores<TInput, TOutput>(TInput input)
           where TInput : GetScoresParams
           where TOutput : PartyScore
        {
            return WebRequest.Post<RowListResponse<TOutput>>(
                UrlMap.GetPartiesScoresUrl(input.Leaderboardid, input.skip, input.limit),
                input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<TOutput>>> GetUsersScores<TInput, TOutput>(TInput input)
            where TInput : GetScoresParams
            where TOutput : UserScore
        {

            return WebRequest.Post<RowListResponse<TOutput>>(
                UrlMap.GetUsersScoresUrl(input.Leaderboardid, input.skip, input.limit),
                input.ToString());
        }
        public Task<WebRequest.ResponseWrapper<RowResponse<TOutput>>> GetCurrentUserScore<TInput, TOutput>(TInput input)
       where TInput : GetCurrentUserScoreParams
       where TOutput : UserScore
        {

            return WebRequest.Post<RowResponse<TOutput>>(UrlMap.GetCurrentUserScoreUrl(input.LeaderboardId), input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<UserScore>>> GetFriendsScores<T>(T input) where T : GetFriendsScoresParams
        {
            return WebRequest.Get<RowListResponse<UserScore>>(UrlMap.GetMyFriendsScoreUrl(input.LeaderboardId));
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TOutput>>> SubmitScore<TInput, TOutput>(TInput input)
           where TInput : SubmitScoreParams
           where TOutput : UserScore
        {
            return WebRequest.Post<RowResponse<TOutput>>(UrlMap.SubmitScoreUrl(input.LeaderboardId), input.ToString());
        }
    }
}