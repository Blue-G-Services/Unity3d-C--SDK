using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Device.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Device.Repositories
{
    public interface IDeviceRepository
    {
        Task<WebRequest.ResponseWrapper<RowListResponse<User.Models.Device>>> FindMyDevices<T>(T param) where T : FindMyDeviceParams;
        Task<WebRequest.ResponseWrapper<ActionResponse>> RevokeDevice<T>(T param) where T : RevokeDeviceParams;
    }
}