using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Authentication.Repositories
{
    public interface IAuthenticationRepositories
    {
        Task<WebRequest.ResponseWrapper<LoginResponse>> RegisterWithEmail<T>(T param) where T : RegisterWithEmailParams;
        Task<WebRequest.ResponseWrapper<LoginResponse>> LoginWithEmail<T>(T param) where T : LoginWithEmailParams;
        Task<WebRequest.ResponseWrapper<LoginResponse>> LoginWithGoogle<T>(T param) where T : LoginWithGoogleParams;
        Task<WebRequest.ResponseWrapper<LoginResponse>> LoginAsGuest<T>(T param) where T : LoginAsGuestParams;
        Task<WebRequest.ResponseWrapper<bool>> IsOtaReady<T>(T param) where T : IsOtaReadyParams;
        Task<WebRequest.ResponseWrapper<ActionResponse>> SendOtaToken<T>(T param) where T : SendOtaTokenParams;
        Task<WebRequest.ResponseWrapper<LoginResponse>> VerifyOtaToken<T>(T param) where T : VerifyOtaTokenParams;
    }
}