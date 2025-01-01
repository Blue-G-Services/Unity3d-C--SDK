using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Friendship.Models;

namespace DynamicPixels.GameService.Services.Friendship
{
    public interface IFriendship
    {
        Task<RowListResponse<Models.Friendship>> GetMyFriends<T>(T input, Action<List<Models.Friendship>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetMyFriendsParams;
        Task<RowListResponse<Models.Friendship>> GetMyFriendshipRequests<T>(T input, Action<List<Models.Friendship>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetMyFriendshipRequestsParams;
        Task<RowResponse<Models.Friendship>> RequestFriendship<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RequestFriendshipParams;
        Task<RowResponse<Models.Friendship>> AcceptRequest<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : AcceptRequestParams;
        Task<RowResponse<Models.Friendship>> RejectRequest<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RejectRequestParams;
        Task<RowResponse<int>> RejectAllRequests<T>(T input, Action<int> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RejectAllRequestsParams;
        Task<RowResponse<bool>> DeleteFriend<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : DeleteFriendParams;
    }
}