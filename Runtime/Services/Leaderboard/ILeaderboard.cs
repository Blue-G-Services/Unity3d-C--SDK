using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Leaderboard.Models;

namespace DynamicPixels.GameService.Services.Leaderboard
{
    public interface ILeaderboard
    {
        Task<RowListResponse<Models.Leaderboard>> GetLeaderboards<T>(T param, Action<List<Models.Leaderboard>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetLeaderboardsParams;
        Task<RowListResponse<TOutput>> GetUsersScores<TInput, TOutput>(TInput param, Action<List<TOutput>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
            where TInput : GetScoresParams where TOutput : UserScore;
        Task<RowListResponse<TOutput>> GetPartiesScores<TInput, TOutput>(TInput param, Action<List<TOutput>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
            where TInput : GetScoresParams where TOutput : PartyScore;
        Task<RowListResponse<UserScore>> GetFriendsScores<T>(T param, Action<List<UserScore>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetFriendsScoresParams;
        Task<RowResponse<TOutput>> GetMyScore<TInput, TOutput>(TInput param, Action<TOutput> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : GetCurrentUserScoreParams
            where TOutput : UserScore;
        Task<RowResponse<TOutput>> SubmitScore<TInput, TOutput>(TInput param, Action<TOutput> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
            where TInput : SubmitScoreParams where TOutput : UserScore;
    }
}