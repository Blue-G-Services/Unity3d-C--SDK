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
    public class Match
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public MatchStatus Status { get; set; }
        public string Metadata { get; set; }
        public IEnumerable<MatchPlayer> Players { get; set; }
        public Room.Room Room { get; set; }

        /// <summary>
        /// Saves the match metadata to the server.
        /// </summary>
        /// <param name="matchMetadata">The metadata to be saved for the match.</param>
        /// <returns>A task representing the asynchronous operation, with the updated match as the result.</returns>
        public async Task<RowResponse<Match>> Save(string matchMetadata, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var data = new { Metadata = matchMetadata };
            var result = await WebRequest.Put<Match>(UrlMap.SaveUrl(Id), JsonConvert.SerializeObject(data));
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
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
                IsSuccessful = result.Successful,
            };
        }

        /// <summary>
        /// Saves a specific state associated with the match to the server.
        /// </summary>
        /// <param name="key">The key identifying the state.</param>
        /// <param name="value">The value of the state to be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> SaveState(string key, string value, Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var data = new { StateData = value };
            var result = await WebRequest.Put(UrlMap.SaveState(Id, key), JsonConvert.SerializeObject(data));
            if (result.Successful)
            {
                successfulCallback?.Invoke();
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new BaseResponse()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                IsSuccessful = result.Successful,
            };
        }

        /// <summary>
        /// Loads a specific state associated with the match from the server.
        /// </summary>
        /// <param name="key">The key identifying the state to be loaded.</param>
        /// <returns>A task representing the asynchronous operation, with the state data as the result.</returns>
        public async Task<RowResponse<string>> LoadState(string key, Action<string> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Get<MatchState>(UrlMap.LoadState(Id, key));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result.StateData);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new RowResponse<string>()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result.StateData,
                IsSuccessful = result.Successful
            };
        }

        /// <summary>
        /// Saves player-specific metadata for the match.
        /// </summary>
        /// <param name="metadata">The metadata to be saved for the player.</param>
        /// <returns>A task representing the asynchronous operation, with the updated player data as the result.</returns>
        public async Task<RowResponse<MatchPlayer>> SavePlayerData(string metadata, Action<MatchPlayer> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var data = new { Metadata = metadata };
            var result = await WebRequest.Put<MatchPlayer>(UrlMap.SavePlayerMetaDataUrl(Id), JsonConvert.SerializeObject(data));
            if (result.Successful)
            {
                successfulCallback?.Invoke(result.Result);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }

            return new RowResponse<MatchPlayer>()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                Row = result.Result,
                IsSuccessful = result.Successful,
            };
        }

        /// <summary>
        /// Starts the match by updating its status to "Started".
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with the updated match as the result.</returns>
        public async Task<RowResponse<Match>> Start(Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await UpdateStatus(MatchStatus.Started);
            if (result.IsSuccessful)
            {
                successfulCallback?.Invoke(result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result;
        }

        /// <summary>
        /// Pauses the match by updating its status to "Paused".
        /// </summary>
        /// <param name="metadata">Additional metadata related to pausing the match.</param>
        /// <returns>A task representing the asynchronous operation, with the updated match as the result.</returns>
        public async Task<RowResponse<Match>> Pause(string metadata, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await Save(metadata);
            if (result.IsSuccessful)
            {
                var response = await UpdateStatus(MatchStatus.Paused);
                if (response.IsSuccessful)
                {
                    successfulCallback?.Invoke(response.Row);
                }
                else
                {
                    failedCallback?.Invoke(response.ErrorCode, response.ErrorMessage);
                }

                return response;
            }
            failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            return result;
        }

        /// <summary>
        /// Resumes the match by updating its status to "Resumed".
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with the updated match as the result.</returns>
        public async Task<RowResponse<Match>> Resume(Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await UpdateStatus(MatchStatus.Resumed);
            if (result.IsSuccessful)
            {
                successfulCallback?.Invoke(result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result;
        }

        /// <summary>
        /// Finishes the match by sending a request to the server to update the match status to "Finished".
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> Finish(Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Patch(UrlMap.FinishMatchUrl(Id));
            if (result.Successful)
            {
                successfulCallback?.Invoke();
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new BaseResponse()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                IsSuccessful = result.Successful,
            };
        }

        /// <summary>
        /// Leaves the match and aborts it by sending a request to the server to update the match status to "Aborted".
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> LeaveAndAbort(Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Delete(UrlMap.LeaveAndAbortUrl(Id));
            if (result.Successful)
            {
                successfulCallback?.Invoke();
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return new BaseResponse()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage,
                IsSuccessful = result.Successful,
            };
        }

        /// <summary>
        /// Updates the status of the match to the specified status.
        /// </summary>
        /// <param name="status">The new status to set for the match.</param>
        /// <returns>A task representing the asynchronous operation, with the updated match as the result.</returns>
        private async Task<RowResponse<Match>> UpdateStatus(MatchStatus status, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var data = new { Status = status };
            var response = await WebRequest.Put<Match>(UrlMap.UpdateMatchStatusUrl(Id), JsonConvert.SerializeObject(data));
            if (response.Successful)
            {
                successfulCallback?.Invoke(response.Result);
            }
            else
            {
                failedCallback?.Invoke(response.ErrorCode, response.ErrorMessage);
            }
            return new RowResponse<Match>()
            {
                ErrorCode = response.ErrorCode,
                ErrorMessage = response.ErrorMessage,
                IsSuccessful = response.Successful,
                Row = response.Result,
            };
        }
    }
    
    public struct MatchPlayer
    {
        public int UserId { get; set; }
        
        public bool? IsTurn { get; set; }
        
        public PlayerState State { get; set; }
        
        public List<string> Tags { get; set; }
        
        public string Metadata { get; set; }
    }
    
    public enum PlayerState
    {
        Init = 1,
        
        Timeout = 2,
        
        LostConnection = 3,
        
        GameOver = 4,
        
        Win = 5,
    }
    
    public enum MatchStatus
    {
        Init = 0,
        
        Started = 1,
        
        Paused = 2,
        
        Resumed = 3,
        
        Finished = 4,
        
        Aborted = 5
    }
}
