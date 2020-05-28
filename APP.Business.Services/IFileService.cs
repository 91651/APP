using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IFileServicer
    {
        string AddFile(FileModel model);
        ResultModel DelFile(string id);
        Task<FileModel> GetFile(string id);
        FileModel GetFileByMd5(string md5);
        bool IsMultipleOwner(string id, string ownerId);
    }
}