using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Utils.HttpClient;
using DynamicPixels.GameService.Utils.WebsocketClient;
using Newtonsoft.Json;

namespace DynamicPixels.GameService.Services.MultiPlayer.Room
{
    public class Room
    {
        private IWebSocketService _socketAgent;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public int MinPlayer { get; set; }
        public int MaxPlayer { get; set; }
        public int? MinXp { get; set; }
        public int? MaxXp { get; set; }
        public bool IsPermanent { get; set; }
        public RoomStatus Status { get; set; }
        public bool IsTurnBasedGame { get; set; }
        public bool IsLocked { get; set; }
        public GameOrderType? GameOrderType { get; set; }
        public string Metadata { get; set; }
        public int CreatorId { get; set; }
        public List<RoomPlayer> Players { get; set; }


        /// <summary>
        /// Event triggered after a user has joined the room.
        /// </summary>
        public event EventHandler<RoomPlayer> OnAfterUserJoinedToRoom;

        /// <summary>
        /// Event triggered when the room is created.
        /// </summary>
        public event EventHandler<Room> OnCreateRoom;

        /// <summary>
        /// Event triggered when the user leaves the room.
        /// </summary>
        public event EventHandler<int> OnLeaveRoom;

        /// <summary>
        /// Event triggered when the room is disposed.
        /// </summary>
        public event EventHandler<int> OnDisposeRoom;

        /// <summary>
        /// Event triggered when the disconnected from the room.
        /// </summary>
        public event EventHandler OnDisconnect;

        /// <summary>
        /// Event triggered when a message is received from the server.
        /// </summary>
        public event EventHandler<Request> OnMessageReceived;
        public event EventHandler<RoomPlayer> OnUserOnline;
        public event EventHandler<RoomPlayer> OnUserOffline;


        /// <summary>
        /// Configures the room with the specified socket agent.
        /// Initializes event handlers for message reception and disconnection.
        /// </summary>
        /// <param name="socketAgent">Socket agent used for communication.</param>
        public void Config(IWebSocketService socketAgent)
        {
            _socketAgent = socketAgent;
            _socketAgent.OnMessageReceived += OnMessage;
            _socketAgent.OnDisconnect += Disconnect;
        }

        /// <summary>
        /// Opens the room by setting its status to "Open".
        /// Sends a PUT request to update the room's status on the server.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> Open(Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var sendingBody = new { State = nameof(RoomStatus.Open) };
            var result = await WebRequest.Put(UrlMap.UpdateStatusUrl(Id), JsonConvert.SerializeObject(sendingBody));
            if (result.Successful)
            {
                Status = RoomStatus.Open;
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
        /// Locks the room by setting its status to "Lock".
        /// Sends a PUT request to update the room's status on the server.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> Lock(Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var sendingBody = new { State = nameof(RoomStatus.Lock) };
            var result = await WebRequest.Put(UrlMap.UpdateStatusUrl(Id), JsonConvert.SerializeObject(sendingBody));
            if (result.Successful)
            {
                Status = RoomStatus.Lock;
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
        /// Sends a message to a specific user in the room.
        /// </summary>
        /// <param name="receiverId">The ID of the receiving user.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task SendToUser(int receiverId, string message)
        {
            var payload = new SendToRoomInputDto
            {
                RoomId = Id,
                Message = message
            };

            var packet = new Request
            {
                Method = "room:send-to-user",
                ReceiverId = receiverId,
                Payload = JsonConvert.SerializeObject(payload)
            };

            return _socketAgent.SendAsync(packet);
        }

        /// <summary>
        /// Broadcasts a message to all users in the room.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task Broadcast(string message)
        {
            var payload = new SendToRoomInputDto
            {
                RoomId = Id,
                Message = message
            };

            var packet = new Request()
            {
                Method = "room:broadcast",
                Payload = JsonConvert.SerializeObject(payload)
            };

            return _socketAgent.SendAsync(packet);
        }

        /// <summary>
        /// Leaves the room.
        /// Sends a DELETE request to remove the user from the room on the server.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<BaseResponse> Leave(Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
        {
            var result = await WebRequest.Delete(UrlMap.LeaveRoomUrl(Id));
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
        private void UserJoinedListenerAction(string message)
        {
            var user = JsonConvert.DeserializeObject<RoomPlayer>(message);

            Players.Add(user);

            OnAfterUserJoinedToRoom?.Invoke(this, user);
        }
        private void UserLeftListenerAction(string message)
        {
            var userId = Convert.ToInt32(message);

            Players.RemoveAll(a => a.UserId == userId);

            OnLeaveRoom?.Invoke(this, userId);
        }
        private void OnMessage(object source, Request packet)
        {
            switch (packet.Method)
            {
                case MessageType.SendToUserMessage:
                case MessageType.BroadcastToRoomMessage:
                    OnMessageReceived?.Invoke(this, packet);
                    break;
                case MessageType.UserJoinedToRoomMessage:
                    UserJoinedListenerAction(packet.Payload);
                    break;
                case MessageType.UserLeftRoomMessage:
                    UserLeftListenerAction(packet.Payload);
                    break;
                case MessageType.UserOnlineMessage:
                    OnUserOnline?.Invoke(this, JsonConvert.DeserializeObject<RoomPlayer>(packet.Payload));
                    break;
                case MessageType.UserOfflineMessage:
                    OnUserOffline?.Invoke(this, JsonConvert.DeserializeObject<RoomPlayer>(packet.Payload));
                    break;
            }
        }
        private void Disconnect(object sender, EventArgs e)
        {
            if (OnDisconnect != null)
                OnDisconnect(sender, null);
        }
    }

    public struct RoomPlayer
    {
        public int UserId { get; set; }
        public string Metadata { get; set; }
        public List<string> Tags { get; set; }
    }

    public enum RoomStatus
    {
        Initial = 0,
        Open = 1,
        Lock = 2
    }

    public enum GameOrderType
    {
        Random = 1,
        RoundRobin = 2,
        UserDefined = 3
    }

    internal static class MessageType
    {
        public const string BroadcastToRoomMessage = "room:broadcast";
        public const string SendToUserMessage = "room:send-to-user";
        public const string RoomCreatedMessage = "room:created";
        public const string RoomDeletedMessage = "room:deleted";
        public const string UserJoinedToRoomMessage = "room:user-joined";
        public const string UserLeftRoomMessage = "room:user-left";
        public const string UserOnlineMessage = "user:online";
        public const string UserOfflineMessage = "user:offline";
    }
    internal class SendToRoomInputDto
    {
        public int RoomId { get; set; }
        public string Message { get; set; }
    }
}
