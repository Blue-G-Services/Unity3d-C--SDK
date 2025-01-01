using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Table.Models;

namespace DynamicPixels.GameService.Services.Table
{
    public interface ITable
    {
        public Task<RowListResponse<TY>> Find<TY, T>(T param, Action<RowListResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindParams where TY : BaseTableModel;
        public Task<RowResponse<TY>> FindById<TY, T>(T param, Action<RowResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindByIdParams where TY : BaseTableModel;
        public Task<RowResponse<TY>> FindByIdAndDelete<TY, T>(T param, Action<RowResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindByIdAndDeleteParams where TY : BaseTableModel;
        public Task<RowResponse<TY>> FindByIdAndUpdate<TY, T>(T param, Action<RowResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : FindByIdAndUpdateParams where TY : BaseTableModel;
        public Task<RowResponse<TY>> Insert<TY, T>(T param, Action<RowResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : InsertParams where TY : BaseTableModel;
        public Task<RowResponse<TY>> InsertMany<TY, T>(T param, Action<RowResponse<TY>> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : InsertManyParams where TY : BaseTableModel;
        public Task<ActionResponse> UpdateMany<T>(T param, Action<ActionResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : UpdateManyParams;
        public Task<ActionResponse> Delete<T>(T param, Action<ActionResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : DeleteParams;
        public Task<ActionResponse> DeleteMany<T>(T param, Action<ActionResponse> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where T : DeleteManyParams;
    }
}