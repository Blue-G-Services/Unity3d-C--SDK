using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.MultiPlayer.Room.Models;

namespace DynamicPixels.GameService.Services.MultiPlayer.Room
{
    public interface IRoomService
    {
        Task<RowResponse<Room>> CreateRoom(CreateRoomParams input, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> CreateAndOpenRoom(CreateRoomParams input, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowListResponse<Room>> GetAllRooms(GetAllRoomsParams inputParams, Action<List<Room>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowListResponse<Room>> GetAllMatchedRooms(GetAllRoomsParams inputParams, Action<List<Room>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> GetRoomById(int roomId, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> GetRoomByName(string name, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> Join(int roomId, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> Join(string roomName, Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<RowResponse<Room>> AutoMatch(Action<Room> successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        void OnMessageReceived(object sender, Request request);
        Task<BaseResponse> Leave(int roomId, Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
        Task<BaseResponse> DeleteRoom(int roomId, Action successfulCallback = null, Action<ErrorCode, string> failedCallback = null);
    }
}