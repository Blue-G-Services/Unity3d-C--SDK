using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Chat.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Chat.Repositories
{
    public class ChatRepository : IChatRepository
    {
        public Task<WebRequest.ResponseWrapper<RowListResponse<Conversation>>> GetSubscribedConversations<T>(T input) where T : GetSubscribedConversationsParams
        {
            return WebRequest.Get<RowListResponse<Conversation>>(UrlMap.GetSubscribedConversationsUrl);
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<Message>>> GetConversationMessages<T>(T input) where T : GetConversationMessagesParams
        {
            return WebRequest.Get<RowListResponse<Message>>(UrlMap.GetConversationMessagesUrl(input.ConversationId));
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<ConversationMember>>> GetConversationMembers<T>(T input) where T : GetConversationMembersParams
        {
            return WebRequest.Get<RowListResponse<ConversationMember>>(UrlMap.GetConversationMembersUrl(input.ConversationId));
        }
    }
}