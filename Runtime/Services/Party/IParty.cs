using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Party.Models;

namespace DynamicPixels.GameService.Services.Party
{
    public interface IParty
    {
        Task<RowListResponse<Party>> GetParties<T>(T param, Action<List<Party>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetPartiesParams;
        Task<RowResponse<Party>> CreateParty<T>(T param, Action<Party> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : CreatePartyParams;
        Task<RowListResponse<Party>> GetSubscribedParties<T>(T param, Action<List<Party>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetSubscribedPartiesParams;
        Task<RowResponse<PartyMember>> JoinToParty<T>(T param, Action<PartyMember> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : JoinToPartyParams;
        Task<RowResponse<bool>> LeaveParty<T>(T param, Action<bool> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : LeavePartyParams;
        Task<RowResponse<Party>> GetPartyById<T>(T param, Action<Party> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetPartyByIdParams;
        Task<RowListResponse<RichPartyMember>> GetPartyMembers<T>(T param, Action<List<RichPartyMember>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetPartyMembersParams;
        Task<RowResponse<PartyMember>> SetMemberVariables<T>(T param, Action<PartyMember> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : SetMemberVariablesParams;
        Task<RowListResponse<PartyMember>> GetPartyWaitingMembers<T>(T param, Action<List<PartyMember>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetPartyWaitingMembersParams;
        Task<RowResponse<Party>> EditParty<T>(T param, Action<Party> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : EditPartyParams;
        Task<RowResponse<PartyMember>> AcceptJoining<T>(T param, Action<PartyMember> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : AcceptJoiningParams;
        Task<RowResponse<PartyMember>> RejectJoining<T>(T param, Action<PartyMember> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : RejectJoiningParams;
    }
}
