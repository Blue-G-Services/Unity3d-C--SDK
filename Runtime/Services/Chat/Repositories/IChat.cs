using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Chat.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Chat.Repositories
{
    public interface IChatRepository
    {
        public Task<WebRequest.ResponseWrapper<RowListResponse<Conversation>>> GetSubscribedConversations<T>(T param) where T : GetSubscribedConversationsParams;
        public Task<WebRequest.ResponseWrapper<RowListResponse<Message>>> GetConversationMessages<T>(T param) where T : GetConversationMessagesParams;
        public Task<WebRequest.ResponseWrapper<RowListResponse<ConversationMember>>> GetConversationMembers<T>(T param) where T : GetConversationMembersParams;

    }

}