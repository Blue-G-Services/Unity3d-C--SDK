using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;

namespace DynamicPixels.GameService.Services.Synchronise.Repositories
{
    public interface ISynchroniseRepositories
    {
        public Task<RowResponse<DateTime>> GetServerTime(Action<DateTime> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
    }
}