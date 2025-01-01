using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Achievement.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Achievement.Repositories
{
    public interface IAchievementRepository
    {
        Task<WebRequest.ResponseWrapper<RowListResponse<Models.Achievement>>> GetAchievements<T>(T param) where T : GetAchievementParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Unlock>>> UnlockAchievement<T>(T param) where T : UnlockAchievementParams;
    }
}