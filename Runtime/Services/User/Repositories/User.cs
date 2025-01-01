using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.User.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.User.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<WebRequest.ResponseWrapper<RowListResponse<Models.User>>> Find<T>(T input) where T : FindUserParams
        {
            return WebRequest.Post<RowListResponse<Models.User>>(UrlMap.FindUsersUrl, input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<Models.User>>> FindUserById<T>(T input) where T : FindUserByIdParams
        {
            return WebRequest.Get<RowResponse<Models.User>>(UrlMap.FindUserByIdUrl(input.UserId));
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<Models.User>>> EditCurrentUser<T>(T input) where T : EditCurrentUserParams
        {
            return WebRequest.Put<RowResponse<Models.User>>(UrlMap.EditCurrentUserUrl, input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> BanUserById<T>(T input) where T : BanUserByIdParams
        {
            return WebRequest.Delete<ActionResponse>(UrlMap.BanUserByIdUrl(input.UserId));
        }
    }
}