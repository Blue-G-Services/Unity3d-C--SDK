using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Models.outputs;

namespace DynamicPixels.GameService.Services.Storage
{
    public interface IStorage
    {
        // private IStorageRepositories _repository;

        void Download();
        Task<RowResponse<FileMetadata>> GetFileInfo(Action<FileMetadata> successfulCallback = null,
            Action<ErrorCode, string> failedCallback = null);
        public Task<RowResponse<FileMetaForUpload>> UploadAsync<TInput>(TInput param, Action<FileMetaForUpload> successfulCallback = null, Action<ErrorCode, string> failedCallback = null) where TInput : FileMetaForUpload;

    }
}