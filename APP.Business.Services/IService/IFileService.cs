using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IFileServicer
    {
        Task<string> AddFileAsync(FileModel model);
        Task<ResultModel> DelFileAsync(string id);
        Task<FileModel> GetFile(string id);
        FileModel GetFileByMd5(string md5);
        bool IsMultipleOwner(string id, string ownerId);
    }
}