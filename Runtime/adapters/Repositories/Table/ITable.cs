using System.Threading.Tasks;
using GameService.Client.Sdk.Models.inputs;
using GameService.Client.Sdk.Models.outputs;

namespace GameService.Client.Sdk.Adapters.Repositories.Table
{
    public interface ITableRepositories
    {
        public Task<RowListResponse<TY>> Find<TY, T>(T param) where T: FindParams;
        public Task<RowResponse<TY>> FindById<TY, T>(T param) where T: FindByIdParams;
        public Task<RowResponse<TY>> FindByIdAndDelete<TY, T>(T param) where T: FindByIdAndDeleteParams;
        public Task<RowResponse<TY>> FindByIdAndUpdate<TY, T>(T param) where T: FindByIdAndUpdateParams;
        public Task<RowResponse<TY>> Insert<TY, T>(T param) where T: InsertParams;
        public Task<RowResponse<TY>> InsertMany<TY, T>(T param) where T: InsertManyParams;
        public Task<ActionResponse> UpdateMany<T>(T param) where T: UpdateManyParams;
        public Task<ActionResponse> Delete<T>(T param) where T: DeleteParams;
        public Task<ActionResponse> DeleteMany<T>(T param) where T: DeleteManyParams;
    }


}