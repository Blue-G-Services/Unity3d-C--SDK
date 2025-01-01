using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Synchronise.Repositories
{
    public class SynchroniseRepository : ISynchroniseRepositories
    {
        public async Task<RowResponse<DateTime>> GetServerTime(Action<DateTime> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var response = await WebRequest.Get<RowResponse<long>>(UrlMap.GetServerTimeUrl);
            if (response.Successful)
            {
                successfulCallback?.Invoke(ConvertUnixTimestampToDateTime(response.Result.Row));   
            }
            else
            {
                failedCallback?.Invoke(response.ErrorCode, response.ErrorMessage);
            }
            return new RowResponse<DateTime>()
            {
                ErrorCode = response.ErrorCode,
                ErrorMessage = response.ErrorMessage,
                Row = ConvertUnixTimestampToDateTime(response.Result.Row),
                IsSuccessful = response.Successful
            };
            
        }
        private DateTime ConvertUnixTimestampToDateTime(long timestamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(timestamp);
        }
    }

}