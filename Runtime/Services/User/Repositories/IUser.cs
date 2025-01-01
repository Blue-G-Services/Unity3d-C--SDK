using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.User.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.User.Repositories
{
    public interface IUserRepository
    {
        public Task<WebRequest.ResponseWrapper<RowListResponse<Models.User>>> Find<T>(T param) where T : FindUserParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<Models.User>>> FindUserById<T>(T param) where T : FindUserByIdParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<Models.User>>> EditCurrentUser<T>(T param) where T : EditCurrentUserParams;
        public Task<WebRequest.ResponseWrapper<ActionResponse>> BanUserById<T>(T param) where T : BanUserByIdParams;

    }
}