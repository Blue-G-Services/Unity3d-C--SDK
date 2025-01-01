using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Achievement.Models;

namespace DynamicPixels.GameService.Services.Achievement
{
    public interface IAchievement
    {
        
        Task<RowListResponse<Models.Achievement>> GetAchievements<T>(T param, Action<List<Models.Achievement>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetAchievementParams;
        Task<RowResponse<Unlock>> UnlockAchievement<T>(T param, Action<Unlock> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : UnlockAchievementParams;

    }
}