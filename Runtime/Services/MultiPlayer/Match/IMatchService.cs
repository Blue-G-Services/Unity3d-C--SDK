using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.MultiPlayer.Match.Models;

namespace DynamicPixels.GameService.Services.MultiPlayer.Match
{
    public interface IMatchService
    {
        Task<RowResponse<Match>> MakeMatch(int roomId, bool lockRoom = true, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Match>> MakeAndStartMatch(int roomId, bool lockRoom = true, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Match>> LoadMatch(int matchId, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Match>> LoadMatchByRoomId(int roomId, Action<Match> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowListResponse<MatchSummary>> GetMyMatches(Action<List<MatchSummary>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
    }
}