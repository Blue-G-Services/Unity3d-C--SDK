using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Party.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Party.Repositories
{
    public interface IPartyRepository
    {
        Task<WebRequest.ResponseWrapper<RowListResponse<Party>>> GetParties<T>(T param) where T : GetPartiesParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Party>>> CreateParty<T>(T param) where T : CreatePartyParams;
        Task<WebRequest.ResponseWrapper<RowListResponse<Party>>> GetSubscribedParties<T>(T param) where T : GetSubscribedPartiesParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Party>>> GetPartyById<T>(T param) where T : GetPartyByIdParams;
        Task<WebRequest.ResponseWrapper<RowResponse<PartyMember>>> JoinToParty<T>(T param) where T : JoinToPartyParams;
        Task<WebRequest.ResponseWrapper<RowResponse<Party>>> EditParty<T>(T param) where T : EditPartyParams;
        Task<WebRequest.ResponseWrapper<ActionResponse>> LeaveParty<T>(T param) where T : LeavePartyParams;
        Task<WebRequest.ResponseWrapper<RowListResponse<RichPartyMember>>> GetPartyMembers<T>(T param) where T : GetPartyMembersParams;
        Task<WebRequest.ResponseWrapper<RowListResponse<PartyMember>>> GetPartyWaitingMembers<T>(T param) where T : GetPartyWaitingMembersParams;
        Task<WebRequest.ResponseWrapper<RowResponse<PartyMember>>> AcceptJoining<T>(T param) where T : AcceptJoiningParams;
        Task<WebRequest.ResponseWrapper<RowResponse<PartyMember>>> RejectJoining<T>(T param) where T : RejectJoiningParams;

    }
}