using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Table.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Table.Repositories
{
    public interface ITableRepositories
    {
        public Task<WebRequest.ResponseWrapper<RowListResponse<TY>>> Find<TY, T>(T param) where T : FindParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindById<TY, T>(T param) where T : FindByIdParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindByIdAndDelete<TY, T>(T param) where T : FindByIdAndDeleteParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindByIdAndUpdate<TY, T>(T param) where T : FindByIdAndUpdateParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> Insert<TY, T>(T param) where T : InsertParams;
        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> InsertMany<TY, T>(T param) where T : InsertManyParams;
        public Task<WebRequest.ResponseWrapper<ActionResponse>> UpdateMany<T>(T param) where T : UpdateManyParams;
        public Task<WebRequest.ResponseWrapper<ActionResponse>> Delete<T>(T param) where T : DeleteParams;
        public Task<WebRequest.ResponseWrapper<ActionResponse>> DeleteMany<T>(T param) where T : DeleteManyParams;
    }


}