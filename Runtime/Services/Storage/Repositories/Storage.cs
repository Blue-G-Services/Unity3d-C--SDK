using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Utils.HttpClient;
using Newtonsoft.Json;

namespace DynamicPixels.GameService.Services.Storage.Repositories
{
    public class StorageRepository : IStorageRepositories
    {
        public Task<FileMetadata> GetFileInfo(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Task Download(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RowResponse<FileMetaForUpload>> UploadFile<T>(T input,
            Action<FileMetaForUpload> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
            where T : FileMetaForUpload
        {
            var fileInfo = new
            {
                file_name = input.Name,
                content_type = input.ContentType
            };

            // Convert the object to JSON
            string json = JsonConvert.SerializeObject(fileInfo, Formatting.Indented);

            var response = await WebRequest.Post<FileMetaForUpload>(UrlMap.GetUploadFileUrl, json);
            if (response.Successful)
            {
                var result = await PutFile(response.Result.row, input.FileContent, input.Name, input.ContentType);
                if (result.Successful)
                {
                    successfulCallback?.Invoke(result.Result);
                }
                else
                {
                    failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
                }

                return new RowResponse<FileMetaForUpload>()
                {
                    IsSuccessful = result.Successful,
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Row = result.Result,
                };
            }
            failedCallback?.Invoke(response.ErrorCode, response.ErrorMessage);
            return new RowResponse<FileMetaForUpload>()
            {
                IsSuccessful = response.Successful,
                ErrorCode = response.ErrorCode,
                ErrorMessage = response.ErrorMessage,
                Row = null,
            };
        }

        public async Task<WebRequest.ResponseWrapper<FileMetaForUpload>> PutFile(string url, byte[] fileContent,
            string Name, string contenttype)
        {
            using (var httpClient = new HttpClient())
            {
                using (var content = new ByteArrayContent(fileContent))
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Put, url))
                    {
                        content.Headers.ContentType = MediaTypeHeaderValue.Parse(contenttype);

                        // Attach the content to the request
                        request.Content = content;

                        // Attach the name as a header or in the request content, depending on your API requirements
                        request.Headers.Add("Name", Name);
                        // Alternatively, you can add it to the content
                        // request.Content.Headers.Add("Name", name);

                        var response = await httpClient.SendAsync(request);
                        var body = await response.Content.ReadAsStringAsync();


                        if (response.IsSuccessStatusCode)
                        {
                            return new WebRequest.ResponseWrapper<FileMetaForUpload>()
                            {
                                Result = JsonConvert.DeserializeObject<FileMetaForUpload>(body), Successful = true
                            };
                        }
                        else
                        {
                            // Deserialize the error response
                            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(body);

                            // Get the corresponding ErrorCode from the error message
                            var errorCode = ErrorMapper.GetErrorCode(errorResponse?.Message ?? string.Empty);

                            // Throw the DynamicPixelsException with the ErrorCode
                            return new WebRequest.ResponseWrapper<FileMetaForUpload>()
                            {
                                Successful = false, ErrorCode = errorCode, ErrorMessage = errorResponse?.Message
                            };
                        }
                    }
                }
            }
        }
    }
}