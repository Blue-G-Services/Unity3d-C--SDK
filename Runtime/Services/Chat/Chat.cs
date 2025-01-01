using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.Chat;
using DynamicPixels.GameService.Services.Chat.Models;
using DynamicPixels.GameService.Services.Chat.Repositories;
using DynamicPixels.GameService.Utils.WebsocketClient;

namespace adapters.services.table.services
{

    public partial class MessageType
    {
        public const string ChatSendPrivate = "chat:send";
        public const string ChatSendGroup = "chat:group:send";
        public const string ChatSubscribe = "chat:subscribe";
        public const string ChatUnsubscribe = "chat:unsubscribe";
        public const string ChatMessageEdit = "chat:message:edit";
        public const string ChatMessageDelete = "chat:message:delete";
        public const string ChatMessagePurge = "chat:message:purge";
        public const string ChatMessageDeleteAll = "chat:group:delete";
    }
    
    public class ChatService: IChat
    {
        // dependencies
        private IChatRepository _repository;
        private readonly IWebSocketService _socketAgent;
        
        public ChatService(IWebSocketService socketAgent)
        {
            _repository = new ChatRepository();
            _socketAgent = socketAgent;


            // TODO: Realtime
            //DynamicPixels.Agent.OnMessageReceived += OnMessage;
        }

        private void OnMessage(object source, Request packet)
        {
            switch (packet.Method)
            {
                case MessageType.ChatSendPrivate:
                    Send(new SendParams{});
                    break;
                case MessageType.ChatSendGroup:
                    Send(new SendParams{});
                    break;
                case MessageType.ChatSubscribe:
                    Subscribe(new SubscribeParams{});
                    break;
                case MessageType.ChatUnsubscribe:
                    Unsubscribe(new UnsubscribeParams{});
                    break;
                case MessageType.ChatMessageEdit:
                    EditMessage(new EditMessageParams{});
                    break;
                case MessageType.ChatMessageDelete:
                    DeleteMessage(new DeleteMessageParams{});
                    break;
                case MessageType.ChatMessagePurge:
                    
                    break;
                case MessageType.ChatMessageDeleteAll:
                    DeleteAllMessage(new DeleteAllMessageParams{});
                    break;
            }
        }

        
        // interactions
        public Task Send<T>(T param) where T : SendParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = param.Type == ConversationType.Private ? MessageType.ChatSendPrivate: MessageType.ChatSendGroup,
                ReceiverId = param.TargetUserId,
                Payload = new Payload
                {
                    TargetId = param.TargetUserId,
                    Message = param.Message
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }

        public Task Subscribe<T>(T param) where T : SubscribeParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = MessageType.ChatSubscribe,
                Payload = new Payload
                {
                    TargetId = param.ConversationId,
                    Value = param.ConversationName,
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }

        public Task Unsubscribe<T>(T param) where T : UnsubscribeParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = MessageType.ChatUnsubscribe,
                Payload = new Payload
                {
                    TargetId = param.ConversationId,
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }
        
        public Task EditMessage<T>(T param) where T : EditMessageParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = MessageType.ChatMessageEdit,
                Payload = new Payload
                {
                    TargetId = param.ConversationId,
                    MessageId = param.MessageId,
                    Message = param.Message,
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }

        public Task DeleteMessage<T>(T param) where T : DeleteMessageParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = MessageType.ChatMessageDelete,
                Payload = new Payload {
                    TargetId = param.ConversationId,
                    MessageId = param.MessageId,
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }

        public Task DeleteAllMessage<T>(T param) where T : DeleteAllMessageParams
        {
            _socketAgent.SendAsync(new Request
            {
                Method = MessageType.ChatMessageDeleteAll,
                Payload = new Payload
                {
                    TargetId = param.ConversationId,
                    SubTargetId = param.TargetUserId,
                }.ToString(),
            });
            
            return Task.CompletedTask;
        }
        
        // https
        public async Task<List<Conversation>> GetSubscribedConversations<T>(T param, Action<List<Conversation>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetSubscribedConversationsParams
        {
            var conversations = await this._repository.GetSubscribedConversations(param);
            conversations.Result.IsSuccessful = conversations.Successful;
            conversations.Result.ErrorCode = conversations.ErrorCode;
            conversations.Result.ErrorMessage = conversations.ErrorMessage;
            if (conversations.Successful)
            {
                successfulCallback?.Invoke(conversations.Result.List);
            }
            else
            {
                failedCallback?.Invoke(conversations.Result.ErrorCode, conversations.ErrorMessage);
            }
            return conversations.Result.List; 
        }

        public async Task<List<Message>> GetConversationMessages<T>(T param, Action<List<Message>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetConversationMessagesParams
        {
            var messages = await this._repository.GetConversationMessages(param);
            messages.Result.IsSuccessful = messages.Successful;
            messages.Result.ErrorCode = messages.ErrorCode;
            messages.Result.ErrorMessage = messages.ErrorMessage;
            if (messages.Successful)
            {
                successfulCallback?.Invoke(messages.Result.List);
            }
            else
            {
                failedCallback?.Invoke(messages.Result.ErrorCode, messages.ErrorMessage);
            }
            return messages.Result.List;
        }

        public async Task<List<ConversationMember>> GetConversationMembers<T>(T param, Action<List<ConversationMember>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : GetConversationMembersParams
        {
            var members = await this._repository.GetConversationMembers(param);
            members.Result.IsSuccessful = members.Successful;
            members.Result.ErrorCode = members.ErrorCode;
            members.Result.ErrorMessage = members.ErrorMessage;
            if (members.Successful)
            {
                successfulCallback?.Invoke(members.Result.List);
            }
            else
            {
                failedCallback?.Invoke(members.Result.ErrorCode, members.ErrorMessage);
            }
            return members.Result.List;
        }

    }
}