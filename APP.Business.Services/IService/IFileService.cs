using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IFileServicer
    {
        Task<string> AddFileAsync(FileModel model);
        Task<bool> DelFileAsync(string id);
        Task<FileModel> GetFileAsync(string id);
        Task<FileModel> GetFileByMd5Async(string md5);
        Task<bool> IsMultipleOwnerAsync(string id, string ownerId);
    }
}