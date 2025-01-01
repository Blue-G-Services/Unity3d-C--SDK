using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;
using DynamicPixels.GameService.Services.Storage.Repositories;

namespace DynamicPixels.GameService.Services.Storage
{
    /// <summary>
    /// Provides storage-related services, including file upload and metadata retrieval.
    /// </summary>
    public class StorageService : IStorage
    {
        private StorageRepository _repository;

        public StorageService()
        {
            _repository = new StorageRepository();
        }

        /// <summary>
        /// Initiates a file download operation.
        /// </summary>
        public void Download()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Uploads a file asynchronously using metadata.
        /// </summary>
        /// <typeparam name="TInput">The type of the file metadata input.</typeparam>
        /// <param name="param">The metadata for the file being uploaded.</param>
        /// <returns>A task that represents the asynchronous operation, returning the uploaded file metadata.</returns>
        public async Task<RowResponse<FileMetaForUpload>> UploadAsync<TInput>(TInput param,
            Action<FileMetaForUpload> successfulCallback = null, Action<ErrorCode, string> failedCallback = null)
            where TInput : FileMetaForUpload
        {
            var result = await _repository.UploadFile(param);
            if (result.IsSuccessful)
            {
                successfulCallback?.Invoke(result.Row);
            }
            else
            {
                failedCallback?.Invoke(result.ErrorCode, result.ErrorMessage);
            }
            return result;
        }

        /// <summary>
        /// Retrieves metadata information about a specific file.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, returning the file metadata.</returns>
        public Task<RowResponse<FileMetadata>> GetFileInfo(Action<FileMetadata> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null)
        {
            throw new System.NotImplementedException();
        }
    }
}