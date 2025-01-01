using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Achievement.Models;
using DynamicPixels.GameService.Services.Achievement.Repositories;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Achievement
{
    public class AchievementService : IAchievement
    {

        private IAchievementRepository _repository;

        public AchievementService()
        {
            _repository = new AchievementRepository();
        }
        /// <summary>
        /// Retrieves a list of achievements for the current user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="GetAchievementParams"/>.</typeparam>
        /// <param name="param">An instance of <typeparamref name="T"/> containing the parameters for retrieving achievements.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Models.Achievement"/> objects representing the user's achievements.</returns>
        public async Task<RowListResponse<Models.Achievement>> GetAchievements<T>(T param, Action<List<Models.Achievement>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetAchievementParams
        {
            var result = await _repository.GetAchievements(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Unlocks an achievement for the current user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="UnlockAchievementParams"/>.</typeparam>
        /// <param name="param">An instance of <typeparamref name="T"/> containing the parameters for unlocking the achievement.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="Unlock"/> object representing the unlocked achievement.</returns>
        public async Task<RowResponse<Unlock>> UnlockAchievement<T>(T param, Action<Unlock> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : UnlockAchievementParams
        {
            var result = await _repository.UnlockAchievement(param);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
    }
}