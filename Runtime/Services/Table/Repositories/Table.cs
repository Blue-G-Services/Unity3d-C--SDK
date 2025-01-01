using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Table.Models;
using DynamicPixels.GameService.Utils.HttpClient;

namespace DynamicPixels.GameService.Services.Table.Repositories
{
    public class TableRepository : ITableRepositories
    {

        public TableRepository()
        {
        }

        public Task<WebRequest.ResponseWrapper<RowListResponse<TY>>> Find<TY, T>(T Params) where T : FindParams
        {
            return WebRequest.Put<RowListResponse<TY>>(UrlMap.FindUrl(Params.tableId, Params.options.Skip, Params.options.Limit), Params.options.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindById<TY, T>(T Params) where T : FindByIdParams
        {
            return WebRequest.Get<RowResponse<TY>>(UrlMap.FindByIdUrl(Params.TableId, Params.RowId));
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindByIdAndDelete<TY, T>(T Params) where T : FindByIdAndDeleteParams
        {
            return WebRequest.Delete<RowResponse<TY>>(UrlMap.FindByIdAndDeleteUrl(Params.TableId, Params.RowId));
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> FindByIdAndUpdate<TY, T>(T Params) where T : FindByIdAndUpdateParams
        {
            return WebRequest.Put<RowResponse<TY>>(UrlMap.FindByIdAndUpdateUrl(Params.TableId, Params.RowId), Params.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> Insert<TY, T>(T Params) where T : InsertParams
        {
            return WebRequest.Post<RowResponse<TY>>(UrlMap.InsertUrl(Params.TableId), Params.ToString());
        }

        public Task<WebRequest.ResponseWrapper<RowResponse<TY>>> InsertMany<TY, T>(T Params) where T : InsertManyParams
        {
            return WebRequest.Post<RowResponse<TY>>(UrlMap.InsertManyUrl(Params.TableId), Params.ToString());
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> UpdateMany<T>(T Params) where T : UpdateManyParams
        {
            return WebRequest.Put<ActionResponse>(UrlMap.UpdateManyUrl(Params.TableId), Params.Options.ToString());
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> Delete<T>(T Params) where T : DeleteParams
        {
            return WebRequest.Post<ActionResponse>(UrlMap.DeleteManyUrl(Params.TableId), Params.ToString());
        }

        public Task<WebRequest.ResponseWrapper<ActionResponse>> DeleteMany<T>(T Params) where T : DeleteManyParams
        {
            return WebRequest.Put<ActionResponse>(UrlMap.DeleteManyUrl(Params.TableId), Params.Options.ToString());
        }
    }
}