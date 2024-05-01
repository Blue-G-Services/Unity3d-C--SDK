using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameService.Client.Sdk.Adapters.Utils.HttpClient;
using GameService.Client.Sdk.Models;
using GameService.Client.Sdk.Models.outputs;
using Newtonsoft.Json;

namespace GameService.Client.Sdk.Adapters.Repositories.Storage
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
        public async Task<FileMetaForUpload> UploadFile<T>(T input) where T : FileMetaForUpload
        {
            var fileInfo = new
            {
                file_name = input.Name,
                content_type = input.ContentType
            };

            // Convert the object to JSON
            string json = JsonConvert.SerializeObject(fileInfo, Formatting.Indented);
           
            var response = await WebRequest.Post(UrlMap.GetUploadFileUrl, json);
            using var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var body = await reader.ReadToEndAsync();
            
            if (response.IsSuccessStatusCode)
            {
                var row = JsonConvert.DeserializeObject<FileMetaForUpload>(body);

                await PutFile(row.row, input.FileContent, input.Name, input.ContentType);

                return JsonConvert.DeserializeObject<FileMetaForUpload>(body);

            }
            else
            {
                // Deserialize the error response
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(body);

                // Get the corresponding ErrorCode from the error message
                var errorCode = ErrorMapper.GetErrorCode(errorResponse?.Message ?? string.Empty);

                // Throw the DynamicPixelsException with the ErrorCode
                throw new DynamicPixelsException(errorCode, errorResponse?.Message);
            }
        }

        public async Task<FileMetaForUpload> PutFile(string url, byte[] fileContent, string Name, string contenttype)
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
                            return JsonConvert.DeserializeObject<FileMetaForUpload>(body);
                        }
                        else
                        {
                            // Deserialize the error response
                            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(body);

                            // Get the corresponding ErrorCode from the error message
                            var errorCode = ErrorMapper.GetErrorCode(errorResponse?.Message ?? string.Empty);

                            // Throw the DynamicPixelsException with the ErrorCode
                            throw new DynamicPixelsException(errorCode, errorResponse?.Message);
                        }
                    }
                }
            }
        }
    }
}


