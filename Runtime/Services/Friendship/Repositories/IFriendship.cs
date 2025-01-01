using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Friendship.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Friendship.Repositories
{
    public interface IFriendshipRepository
    {
        Task<WebRequest.ResponseWrapper<RowListResponse<Models.Friendship>>> GetMyFriends<T>(T param) where T : GetMyFriendsParams;
        Task<WebRequest.ResponseWrapper<RowListResponse<Models.Friendship>>> GetMyFriendshipRequests<T>(T param) where T : GetMyFriendshipRequestsParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> RequestFriendship<T>(T param) where T : RequestFriendshipParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> AcceptRequest<T>(T param) where T : AcceptRequestParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Models.Friendship>>> RejectRequest<T>(T param) where T : RejectRequestParams;
        Task<WebRequest.ResponseWrapper<ActionResponse>> RejectAllRequests<T>(T param) where T : RejectAllRequestsParams;
        Task<WebRequest.ResponseWrapper<ActionResponse>> DeleteFriend<T>(T param) where T : DeleteFriendParams;
    }

}