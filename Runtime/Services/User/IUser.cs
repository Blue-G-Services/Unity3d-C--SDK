using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.User.Models;

namespace DynamicPixels.GameService.Services.User
{
    public interface IUser
    {
        Task<List<Models.User>> Find<T>(T input, Action<List<Models.User>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindUserParams;
        Task<Models.User> GetCurrentUser(Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<Models.User> FindUserById<T>(T input, Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindUserByIdParams;
        Task<Models.User> EditCurrentUser<T>(T input, Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : EditCurrentUserParams;
    }
}