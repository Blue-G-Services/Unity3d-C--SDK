using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.MultiPlayer.Match.Models;
using DynamicPixels.GameService.Utils.HttpClient;
using Newtonsoft.Json;

namespace DynamicPixels.GameService.Services.MultiPlayer.Match
{
    /// <summary>
    /// Provides services for managing multiplayer matches, including creating, starting, and loading matches.
    /// </summary>
    public class MatchService : IMatchService
    {
        /// <summary>
        /// Creates a new match in the specified room.
        /// </summary>
        /// <param name="roomId">The ID of the room where the match will be created.</param>
        /// <param name="lockRoom">
        /// Specifies whether to lock the room after creating the match.
        /// Default is <c>true</c>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result containing the created match details.
        /// </returns>
        public async Task<RowResponse<Match>> MakeMatch(int roomId, bool lockRoom = true, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var input = new MakeMatchParams
            {
                RoomId = roomId,
                LockRoom = lockRoom
            };
            var result = await WebRequest.Post<Match>(UrlMap.MakeMatchUrl, JsonConvert.SerializeObject(input));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowResponse<Match>()
            {
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
            };
        }

        /// <summary>
        /// Creates and starts a new match in the specified room.
        /// </summary>
        /// <param name="roomId">The ID of the room where the match will be created and started.</param>
        /// <param name="lockRoom">
        /// Specifies whether to lock the room after creating the match.
        /// Default is <c>true</c>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result containing the started match details.
        /// </returns>
        public async Task<RowResponse<Match>> MakeAndStartMatch(int roomId, bool lockRoom = true, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var input = new MakeMatchParams
            {
                RoomId = roomId,
                LockRoom = lockRoom
            };
            var result = await WebRequest.Post<Match>(UrlMap.MakeAndStartMatchUrl, JsonConvert.SerializeObject(input));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowResponse<Match>()
            {
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
            };
        }

        /// <summary>
        /// Loads the details of a specific match by its ID.
        /// </summary>
        /// <param name="matchId">The ID of the match to load.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a result containing the match details.
        /// </returns>
        public async Task<RowResponse<Match>> LoadMatch(int matchId, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Get<Match>(UrlMap.LoadMatch(matchId));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowResponse<Match>()
            {
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
            };
        }

        public async Task<RowResponse<Match>> LoadMatchByRoomId(int roomId, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Get<Match>(UrlMap.LoadMatchByRoomId(roomId));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowResponse<Match>()
            {
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
            };
        }

        /// <summary>
        /// Retrieves a list of matches associated with the current user.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, with a result containing a list of match summaries.
        /// </returns>
        public async Task<RowListResponse<MatchSummary>> GetMyMatches(Action<List<MatchSummary>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Get<List<MatchSummary>>(UrlMap.GetMyMatchesUrl);
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowListResponse<MatchSummary>()
            {
                IsSuccessful = result.Successful,
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                List = result.Result,
            };
        }
    }
}
