using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Leaderboard.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Leaderboard.Repositories
{
    public interface ILeaderboardRepository
    {
        Task<WebRequest.ResponseWrapper<RowListResponse<Models.Leaderboard>>> GetLeaderBoards<T>(T param) where T : GetLeaderboardsParams;
        Task<WebRequest.ResponseWrapper<RowListResponse<TOutput>>> GetUsersScores<TInput, TOutput>(TInput param)
            where TInput : GetScoresParams
            where TOutput : UserScore;
        Task<WebRequest.ResponseWrapper<RowListResponse<TOutput>>> GetPartiesScores<TInput, TOutput>(TInput param)
            where TInput : GetScoresParams
            where TOutput : PartyScore;
        Task<WebRequest.ResponseWrapper<RowResponse<TOutput>>> GetCurrentUserScore<TInput, TOutput>(TInput param)
            where TInput : GetCurrentUserScoreParams
            where TOutput : UserScore;
        Task<WebRequest.ResponseWrapper<RowListResponse<UserScore>>> GetFriendsScores<T>(T param) where T : GetFriendsScoresParams;
        Task<WebRequest.ResponseWrapper<RowResponse<TOutput>>> SubmitScore<TInput, TOutput>(TInput param)
            where TInput : SubmitScoreParams
            where TOutput : UserScore;

    }

}