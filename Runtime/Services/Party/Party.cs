using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Party.Models;
using DynamicPixels.GameService.Services.Party.Repositories;

namespace DynamicPixels.GameService.Services.Party
{
    public class PartyService : IParty
    {
        private IPartyRepository _repository;
        public PartyService()
        {
            _repository = new PartyRepository();
        }
        /// <summary>
        /// Retrieves a list of parties based on the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used to filter the parties.</typeparam>
        /// <param name="param">The filtering parameters.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of parties.</returns>
        public async Task<RowListResponse<Party>> GetParties<T>(T param, Action<List<Party>> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : GetPartiesParams
        {
            var result = await _repository.GetParties(param);
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
            return result.Result;
        }
        
        /// <summary>
        /// Creates a new party based on the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used to create the party.</typeparam>
        /// <param name="param">The parameters for the party creation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the created party.</returns>
        public async Task<RowResponse<Party>> CreateParty<T>(T param, Action<Party> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : CreatePartyParams
        {
            var result = await _repository.CreateParty(param);
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
            return result.Result;
        }
        /// <summary>
        /// Retrieves a list of parties the user has subscribed to.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used to filter the subscribed parties.</typeparam>
        /// <param name="param">The filtering parameters.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of subscribed parties.</returns>
        public async Task<RowListResponse<Party>> GetSubscribedParties<T>(T param, Action<List<Party>> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : GetSubscribedPartiesParams
        {
            var result = await _repository.GetSubscribedParties(param);
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
            return result.Result;
        }
        /// <summary>
        /// Joins a user to a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for joining the party.</typeparam>
        /// <param name="param">The parameters for joining the party.</param>
        /// <returns>A task that represents the asynchronous operation, containing the joined party member.</returns>
        public async Task<RowResponse<PartyMember>> JoinToParty<T>(T param, Action<PartyMember> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : JoinToPartyParams
        {
            var result = await _repository.JoinToParty(param);
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
            return result.Result;
        }
        /// <summary>
        /// Removes a user from a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for leaving the party.</typeparam>
        /// <param name="param">The parameters for leaving the party.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean indicating success.</returns>
        public async Task<RowResponse<bool>> LeaveParty<T>(T param, Action<bool> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : LeavePartyParams
        {
            var result = await _repository.LeaveParty(param);
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
        /// Retrieves a party by its unique identifier.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for fetching the party.</typeparam>
        /// <param name="param">The parameters for fetching the party.</param>
        /// <returns>A task that represents the asynchronous operation, containing the requested party.</returns>
        public async Task<RowResponse<Party>> GetPartyById<T>(T param, Action<Party> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : GetPartyByIdParams
        {
            var result = await _repository.GetPartyById(param);
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
            return result.Result;
        }
        /// <summary>
        /// Retrieves a list of members in a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used to fetch party members.</typeparam>
        /// <param name="param">The parameters for fetching the party members.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of party members with additional details.</returns>
        public async Task<RowListResponse<RichPartyMember>> GetPartyMembers<T>(T param, Action<List<RichPartyMember>> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : GetPartyMembersParams
        {
            var result = await _repository.GetPartyMembers(param);
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
            return result.Result;
        }
        /// <summary>
        /// Updates variables associated with a party member.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for updating member variables.</typeparam>
        /// <param name="param">The parameters for updating member variables.</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated party member.</returns>
        public async Task<RowResponse<PartyMember>> SetMemberVariables<T>(T param, Action<PartyMember> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : SetMemberVariablesParams
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Retrieves a list of members waiting for approval to join a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for fetching waiting members.</typeparam>
        /// <param name="param">The parameters for fetching waiting members.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of waiting members.</returns>
        public async Task<RowListResponse<PartyMember>> GetPartyWaitingMembers<T>(T param, Action<List<PartyMember>> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : GetPartyWaitingMembersParams
        {
            var result = await _repository.GetPartyWaitingMembers(param);
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
            return result.Result;
        }
        /// <summary>
        /// Edits the details of a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for editing the party.</typeparam>
        /// <param name="param">The parameters for editing the party.</param>
        /// <returns>A task that represents the asynchronous operation, containing the updated party.</returns>
        public async Task<RowResponse<Party>> EditParty<T>(T param, Action<Party> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : EditPartyParams
        {
            var result = await _repository.EditParty(param);
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
            return result.Result;
        }
        /// <summary>
        /// Accepts a member's request to join a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for accepting the request.</typeparam>
        /// <param name="param">The parameters for accepting the request.</param>
        /// <returns>A task that represents the asynchronous operation, containing the accepted party member.</returns>
        public async Task<RowResponse<PartyMember>> AcceptJoining<T>(T param, Action<PartyMember> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : AcceptJoiningParams
        {
            var result = await _repository.AcceptJoining(param);
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
            return result.Result;
        }
        /// <summary>
        /// Rejects a member's request to join a specified party.
        /// </summary>
        /// <typeparam name="T">The type of the parameters used for rejecting the request.</typeparam>
        /// <param name="param">The parameters for rejecting the request.</param>
        /// <returns>A task that represents the asynchronous operation, containing the rejected party member.</returns>
        public async Task<RowResponse<PartyMember>> RejectJoining<T>(T param, Action<PartyMember> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null) where T : RejectJoiningParams
        {
            var result = await _repository.RejectJoining(param);
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
            return result.Result;
        }
    }
}