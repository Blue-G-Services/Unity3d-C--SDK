using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Authentication.Models;
using DynamicPixels.GameService.Services.Authentication.Repositories;

namespace DynamicPixels.GameService.Services.Authentication
{
    /// <summary>
    /// Provides authentication services for user registration, login, and session management.
    /// </summary>
    public class AuthenticationService : IAuthentication
    {
        private AuthenticationRepository _repository;
        public AuthenticationService()
        {
            _repository = new AuthenticationRepository();
        }

        /// <summary>
        /// Configures the SDK with user session details.
        /// </summary>
        /// <param name="token">The authentication token.</param>
        /// <param name="user">The authenticated user details.</param>
        /// <param name="connInfo">The connection information for establishing a session.</param>
        /// <returns>A task representing the completion of the setup process.</returns>
        private Task SetupSdk(string token, User.Models.User user, ConnectionInfo connInfo)
        {
            ServiceHub.IsAvailable = true;
            ServiceHub.Token = token;
            ServiceHub.User = user;

            // Setup connection based on protocol
            switch (connInfo?.Protocol)
            {
                case "wss":
                    ServiceHub.Agent.ConnectAsync(token);
                    break;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Registers a new user with an email address.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The registration parameters.</param>
        /// <returns>A task representing the registration process, with a result containing the login response.</returns>
        public async Task<LoginResponse> RegisterWithEmail<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RegisterWithEmailParams
        {
            var result = await _repository.RegisterWithEmail(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(result.Result.Token, result.Result.User, result.Result.Connection);
            return result.Result;
        }

        /// <summary>
        /// Logs in a user with an email address.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The login parameters.</param>
        /// <returns>A task representing the login process, with a result containing the login response.</returns>
        public async Task<LoginResponse> LoginWithEmail<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithEmailParams
        {
            var result = await _repository.LoginWithEmail(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(result.Result.Token, result.Result.User, result.Result.Connection);
            return result.Result;
        }

        /// <summary>
        /// Logs in a user with a Google account.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The Google login parameters.</param>
        /// <returns>A task representing the login process, with a result containing the login response.</returns>
        public async Task<LoginResponse> LoginWithGoogle<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithGoogleParams
        {
            var result = await _repository.LoginWithGoogle(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(result.Result.Token, result.Result.User, result.Result.Connection);
            return result.Result;
        }

        /// <summary>
        /// Logs in a user as a guest.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The guest login parameters.</param>
        /// <returns>A task representing the login process, with a result containing the login response.</returns>
        public async Task<LoginResponse> LoginAsGuest<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginAsGuestParams
        {
            var result = await _repository.LoginAsGuest(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(result.Result.Token, result.Result.User, result.Result.Connection);
            return result.Result;
        }

        /// <summary>
        /// Logs in a user using an authentication token.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The token login parameters.</param>
        /// <returns>A task representing the login process, with a result containing the login response.</returns>
        public async Task<LoginResponse> LoginWithToken<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LoginWithTokenParams
        {
            var result = await _repository.LoginWithToken(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(input.Token, new User.Models.User(), new ConnectionInfo());
            return result.Result;
        }

        /// <summary>
        /// Checks if One-Time Authentication (OTA) is ready.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The OTA readiness parameters.</param>
        /// <returns>A task representing the readiness check, with a result indicating if OTA is ready.</returns>
        public async Task<RowResponse<bool>> IsOtaReady<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : IsOtaReadyParams
        {
            var result = await _repository.IsOtaReady(input);
            
            
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new RowResponse<bool>()
            {
                Row = result.Result,
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
            };
        }

        /// <summary>
        /// Sends a One-Time Authentication (OTA) token to the user.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The parameters for sending the OTA token.</param>
        /// <returns>
        /// A task representing the process, with a result indicating if the token was successfully sent.
        /// </returns>
        public async Task<RowResponse<bool>> SendOtaToken<T>(T input, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : SendOtaTokenParams
        {
            var result = await _repository.SendOtaToken(input);
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.Affected > 0);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new RowResponse<bool>()
            {
                Row = result.Result.Affected > 0,
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
            };
        }

        /// <summary>
        /// Verifies the One-Time Authentication (OTA) token.
        /// </summary>
        /// <typeparam name="T">The type of the input parameters.</typeparam>
        /// <param name="input">The parameters for verifying the OTA token.</param>
        /// <returns>A task representing the verification process, with a result containing the login response.</returns>
        public async Task<LoginResponse> VerifyOtaToken<T>(T input, Action<LoginResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : VerifyOtaTokenParams
        {
            var result = await _repository.VerifyOtaToken(input);
            result.Result.IsSuccessful = result.Successful;
            result.Result.ErrorCode = result.ErrorCode;
            result.Result.ErrorMessage = result.ErrorMessage;
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            await SetupSdk(result.Result.Token, result.Result.User, result.Result.Connection);
            return result.Result;
        }

        /// <summary>
        /// Checks if the user is currently logged in.
        /// </summary>
        /// <returns>True if the user is logged in; otherwise, false.</returns>
        public bool IsLoggedIn()
        {
            return ServiceHub.Token != string.Empty;
        }

        /// <summary>
        /// Logs out the current user and clears session data.
        /// </summary>
        public void Logout()
        {
            ServiceHub.IsAvailable = false;
            ServiceHub.Token = string.Empty;
            ServiceHub.User = null;

            // Dispose connections (if applicable).
            // TODO: Realtime support for disconnecting agents.
            // DynamicPixels.Agent.Disconnect();
        }
    }
}
