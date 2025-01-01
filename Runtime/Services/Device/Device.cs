using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Chat.Repositories;
using DynamicPixels.GameService.Services.Device.Models;
using DynamicPixels.GameService.Services.Device.Repositories;

namespace DynamicPixels.GameService.Services.Device
{
    /// <summary>
    /// Provides services for managing devices, including finding associated devices and revoking access.
    /// </summary>
    public class DeviceService : IDevice
    {
        private IDeviceRepository _repository;
        public DeviceService()
        {
            _repository = new DeviceRepository();
        }

        /// <summary>
        /// Retrieves the list of devices associated with the current user.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The parameters used to filter devices.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result containing a list of the user's devices.
        /// </returns>
        public async Task<RowListResponse<User.Models.Device>> FindMyDevices<T>(T input, Action<List<User.Models.Device>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindMyDeviceParams
        {
            var result = await _repository.FindMyDevices(input);
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
        /// Revokes access for a specific device based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The parameters identifying the device to revoke.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result indicating whether the device was successfully revoked.
        /// </returns>
        public async Task<RowResponse<bool>> RevokeDevice<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RevokeDeviceParams
        {
            var result = await _repository.RevokeDevice(input);
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Affected > 0);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new RowResponse<bool>()
            {
                Row = result.Result.Affected > 0,
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
            };
        }
    }
}
