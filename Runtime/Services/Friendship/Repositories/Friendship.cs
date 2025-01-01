using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Friendship.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Friendship.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {

        public Task<WebRequest.ResponseWrapper<RowListResponse<Models.Friendship>>> GetMyFriends<T>(T input) where T : GetMyFriendsParams
        {
            return WebRequest.Get<RowListResponse<Models.Friendship>>(UrlMap.GetMyFriendsUrl);

        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<Models.Friendship>>> GetMyFriendshipRequests<T>(T input) where T : GetMyFriendshipRequestsParams
        {
            return WebRequest.Get<RowListResponse<Models.Friendship>>(UrlMap.GetMyFriendshipRequestsUrl);
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> RequestFriendship<T>(T input) where T : RequestFriendshipParams
        {
            return WebRequest.Post<RowResponse<Models.Friendship>>(UrlMap.RequestFriendshipUrl, input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> AcceptRequest<T>(T input) where T : AcceptRequestParams
        {
            return WebRequest.Post<RowResponse<Models.Friendship>>(UrlMap.AcceptRequestUrl(input.RequestId), input.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> RejectRequest<T>(T input) where T : RejectRequestParams
        {
            return WebRequest.Delete<RowResponse<Models.Friendship>>(UrlMap.RejectRequestUrl(input.RequestId));
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> RejectAllRequests<T>(T input) where T : RejectAllRequestsParams
        {
            return WebRequest.Delete<ActionResponse>(UrlMap.RejectAllRequestsUrl);
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> DeleteFriend<T>(T input) where T : DeleteFriendParams
        {
            return WebRequest.Delete<ActionResponse>(UrlMap.DeleteFriendUrl(input.UserId));
        }
    }
}