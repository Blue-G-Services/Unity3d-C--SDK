using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Authentication.Models;

namespace DynamicPixels.GameService.Services.Authentication
{
    public interface IAuthentication
    {
        Task<LoginResponse> RegisterWithEmail<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RegisterWithEmailParams;
        Task<LoginResponse> LoginWithEmail<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithEmailParams;
        Task<LoginResponse> LoginWithGoogle<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithGoogleParams;
        Task<LoginResponse> LoginAsGuest<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginAsGuestParams;
        Task<LoginResponse> LoginWithToken<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithTokenParams;
        Task<RowResponse<bool>> IsOtaReady<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : IsOtaReadyParams;
        Task<RowResponse<bool>> SendOtaToken<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : SendOtaTokenParams;
        Task<LoginResponse> VerifyOtaToken<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : VerifyOtaTokenParams;
        bool IsLoggedIn();
        void Logout();
    }
}