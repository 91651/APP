using System.Collections.Generic;
using APP.Framework.Services.Models;

namespace APP.Framework.Services
{
    public interface IFileServicer
    {
        string AddFile(FileModel model);
        ResultModel DelFile(string id);
        FileModel GetFile(string id);
        FileModel GetFileByMd5(string md5);
        bool IsMultipleOwner(string id, string ownerId);
    }
}