using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.User.Models;
using DynamicPixels.GameService.Services.User.Repositories;

namespace DynamicPixels.GameService.Services.User
{
    /// <summary>
    /// Provides user-related services, including retrieving, editing, and managing user information.
    /// </summary>
    public class UserService : IUser
    {
        private readonly IUserRepository _repository;
        
        public UserService()
        {
            _repository = new UserRepository();
        }

        /// <summary>
        /// Finds users based on the provided parameters.
        /// </summary>
        /// <typeparam name="T">The type of parameters used for finding users.</typeparam>
        /// <param name="input">The parameters used to search for users.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of users.</returns>
        public async Task<List<Models.User>> Find<T>(T input, Action<List<Models.User>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindUserParams
        {
            var result = await _repository.Find(input);
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
            return result.Result.List;
        }

        /// <summary>
        /// Retrieves the currently logged-in user.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of the current user.</returns>
        public async Task<Models.User> GetCurrentUser(Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await _repository.FindUserById(new FindUserByIdParams
            {
                UserId = ServiceHub.User.Id
            });
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return result.Result.Row;
        }

        /// <summary>
        /// Finds a user by their unique identifier.
        /// </summary>
        /// <typeparam name="T">The type of parameters used for finding a user by ID.</typeparam>
        /// <param name="input">The parameters used to find the user by ID.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the user found.</returns>
        public async Task<Models.User> FindUserById<T>(T input, Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindUserByIdParams
        {
            var result = await _repository.FindUserById(new FindUserByIdParams
            {
                UserId = input.UserId
            });
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result.Result.Row;
        }

        /// <summary>
        /// Updates the details of the currently logged-in user.
        /// </summary>
        /// <typeparam name="T">The type of parameters used for editing the current user.</typeparam>
        /// <param name="input">The parameters containing the updated user data.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the updated user.</returns>
        public async Task<Models.User> EditCurrentUser<T>(T input, Action<Models.User> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : EditCurrentUserParams
        {
            var result = await _repository.EditCurrentUser(new EditCurrentUserParams
            {
                Data = input.Data
            });
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result.Result.Row;
        }

        /// <summary>
        /// Bans a user by their unique identifier.
        /// </summary>
        /// <typeparam name="T">The type of parameters used for banning a user by ID.</typeparam>
        /// <param name="input">The parameters containing the ID of the user to ban.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating whether the user was successfully banned.</returns>
        public async Task<bool> BanUserById<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : BanUserByIdParams
        {
            var result = await _repository.BanUserById(new BanUserByIdParams
            {
                UserId = input.UserId
            });
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Affected > 0);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result.Result.Affected > 0;
        }
    }
}
