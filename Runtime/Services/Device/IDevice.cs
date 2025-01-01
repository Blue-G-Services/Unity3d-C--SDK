using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Device.Models;

namespace DynamicPixels.GameService.Services.Device
{
    public interface IDevice
    {
        Task<RowListResponse<User.Models.Device>> FindMyDevices<T>(T param, Action<List<User.Models.Device>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindMyDeviceParams;
        Task<RowResponse<bool>> RevokeDevice<T>(T param, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RevokeDeviceParams;
    }
}