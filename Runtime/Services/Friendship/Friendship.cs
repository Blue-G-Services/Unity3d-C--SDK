using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Friendship.Models;
using DynamicPixels.GameService.Services.Friendship.Repositories;

namespace DynamicPixels.GameService.Services.Friendship
{
    public class FriendshipService : IFriendship
    {
        private IFriendshipRepository _repository;
        public FriendshipService()
        {
            _repository = new FriendshipRepository();
        }
        /// <summary>
        /// Retrieves a list of friendships for the current user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="GetMyFriendsParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for retrieving friends.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Models.Friendship"/> objects representing the user's friends.</returns>
        public async Task<RowListResponse<Models.Friendship>> GetMyFriends<T>(T input, Action<List<Models.Friendship>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetMyFriendsParams
        {
            var result = await _repository.GetMyFriends(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Retrieves a list of friendship requests received by the current user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="GetMyFriendshipRequestsParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for retrieving friendship requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Models.Friendship"/> objects representing the friendship requests.</returns>
        public async Task<RowListResponse<Models.Friendship>> GetMyFriendshipRequests<T>(T input, Action<List<Models.Friendship>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetMyFriendshipRequestsParams
        {
            var result = await _repository.GetMyFriendshipRequests(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.List);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Sends a friendship request to another user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="RequestFriendshipParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for the friendship request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Models.Friendship"/> object representing the initiated friendship request.</returns>
        public async Task<RowResponse<Models.Friendship>> RequestFriendship<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RequestFriendshipParams
        {
            var result = await _repository.RequestFriendship(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Accepts a friendship request based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="AcceptRequestParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for accepting the friendship request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Models.Friendship"/> object representing the accepted friendship request.</returns>
        public async Task<RowResponse<Models.Friendship>> AcceptRequest<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : AcceptRequestParams
        {
            var result = await _repository.AcceptRequest(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Rejects a friendship request based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="RejectRequestParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for rejecting the friendship request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Models.Friendship"/> object representing the rejected friendship request.</returns>
        public async Task<RowResponse<Models.Friendship>> RejectRequest<T>(T input, Action<Models.Friendship> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RejectRequestParams
        {
            var result = await _repository.RejectRequest(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return result.Result;
        }
        /// <summary>
        /// Rejects all pending friendship requests based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="RejectAllRequestsParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for rejecting all friendship requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of rejected friendship requests.</returns>
        public async Task<RowResponse<int>> RejectAllRequests<T>(T input, Action<int> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RejectAllRequestsParams
        {
            var result = await _repository.RejectAllRequests(input);
            
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Affected);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
            }
            return new RowResponse<int>()
            {
                Row = result.Result.Affected,
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
            };
        }
        /// <summary>
        /// Deletes a friendship with a specified user based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of the input parameter, which must derive from <see cref="DeleteFriendParams"/>.</typeparam>
        /// <param name="input">An instance of <typeparamref name="T"/> containing the parameters for deleting the friend.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean value indicating whether the friend was successfully deleted.</returns>
        public async Task<RowResponse<bool>> DeleteFriend<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : DeleteFriendParams
        {
            var result = await _repository.DeleteFriend(input);
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Affected > 0);
            }
            else
            {
                failedCallback?.Invoke(result.Result.ErrorCode, result.ErrorMessage);
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