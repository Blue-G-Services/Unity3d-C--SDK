using System;
using System.Threading.Tasks;
using DynamicPixels.GameService.Models.outputs;

namespace DynamicPixels.GameService.Services.Synchronise
{
    public interface ISynchronise
    {
        public Task<RowResponse<DateTime>> GetServerTime();
    }
}