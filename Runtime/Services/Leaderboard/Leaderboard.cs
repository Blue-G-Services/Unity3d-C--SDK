using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Leaderboard.Models;
using DynamicPixels.GameService.Services.Leaderboard.Repositories;

namespace DynamicPixels.GameService.Services.Leaderboard
{
    public class LeaderboardService : ILeaderboard
    {
        private ILeaderboardRepository _repository;
        public LeaderboardService()
        {
            _repository = new LeaderboardRepository();
        }
        /// <summary>
        /// Retrieves a list of leaderboards based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, constrained to GetLeaderboardsParams.</typeparam>
        /// <param name="param">The parameters used to filter and fetch the leaderboards.</param>
        /// <returns>A task representing the asynchronous operation, with a list of Leaderboards as the result.</returns>
        public async Task<RowListResponse<Models.Leaderboard>> GetLeaderboards<T>(T param, Action<List<Models.Leaderboard>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetLeaderboardsParams
        {
            var result = await _repository.GetLeaderBoards(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Fetches scores for different parties according to the given parameters.
        /// </summary>
        /// <typeparam name="TInput">The type of the input parameter, constrained to GetScoresParams.</typeparam>
        /// <typeparam name="TOutput">The type of the output result, constrained to PartyScore.</typeparam>
        /// <param name="param">The parameters used to filter and fetch party scores.</param>
        /// <returns>A task representing the asynchronous operation, with a list of PartyScores as the result.</returns>
        public async Task<RowListResponse<TOutput>> GetPartiesScores<TInput, TOutput>(TInput param, Action<List<TOutput>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : GetScoresParams where TOutput : PartyScore
        {
            var result = await _repository.GetPartiesScores<TInput, TOutput>(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Obtains scores for individual users as specified by the input parameters.
        /// </summary>
        /// <typeparam name="TInput">The type of the input parameter, constrained to GetScoresParams.</typeparam>
        /// <typeparam name="TOutput">The type of the output result, constrained to UserScore.</typeparam>
        /// <param name="param">The parameters used to filter and fetch user scores.</param>
        /// <returns>A task representing the asynchronous operation, with a list of UserScores as the result.</returns>
        public async Task<RowListResponse<TOutput>> GetUsersScores<TInput, TOutput>(TInput param, Action<List<TOutput>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : GetScoresParams where TOutput : UserScore
        {
            var result = await _repository.GetUsersScores<TInput, TOutput>(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Gathers scores specifically for friends of the current user.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, constrained to GetFriendsScoresParams.</typeparam>
        /// <param name="param">The parameters used to filter and fetch friends' scores.</param>
        /// <returns>A task representing the asynchronous operation, with a list of UserScores as the result.</returns>
        public async Task<RowListResponse<UserScore>> GetFriendsScores<T>(T param, Action<List<UserScore>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetFriendsScoresParams
        {
            var result = await _repository.GetFriendsScores(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Retrieves the score of the current user based on the input parameters.
        /// </summary>
        /// <typeparam name="TInput">The type of the input parameter, constrained to GetCurrentUserScoreParams.</typeparam>
        /// <typeparam name="TOutput">The type of the output result, constrained to UserScore.</typeparam>
        /// <param name="param">The parameters used to fetch the current user's score.</param>
        /// <returns>A task representing the asynchronous operation, with the user's score as the result.</returns>
        public async Task<RowResponse<TOutput>> GetMyScore<TInput, TOutput>(TInput param, Action<TOutput> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : GetCurrentUserScoreParams where TOutput : UserScore
        {
            var result = await _repository.GetCurrentUserScore<TInput, TOutput>(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Submits a user's score and returns the result.
        /// </summary>
        /// <typeparam name="TInput">The type of the input parameter, constrained to SubmitScoreParams.</typeparam>
        /// <typeparam name="TOutput">The type of the output result, constrained to UserScore.</typeparam>
        /// <param name="param">The parameters used to submit the user's score.</param>
        /// <returns>A task representing the asynchronous operation, with the submitted score as the result.</returns>
        public async Task<RowResponse<TOutput>> SubmitScore<TInput, TOutput>(TInput param, Action<TOutput> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : SubmitScoreParams where TOutput : UserScore
        {
            var result = await _repository.SubmitScore<TInput, TOutput>(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
    }
}