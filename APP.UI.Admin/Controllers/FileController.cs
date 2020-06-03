using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using APP.Business.Services;
using APP.Business.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NJsonSchema.Annotations;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IFileServicer _fileServicer;

        public FileController(IWebHostEnvironment hostEnvironment, IConfiguration configuration, IFileServicer fileServicer)
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
            _fileServicer = fileServicer;
        }

        [HttpPost]
        public async Task<FileModel> Upload(IFormFile file)
        {
            var model = new FileModel();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var md5 = default(string);
                using (var _md5 = MD5.Create())
                {
                    md5 = string.Join("", _md5.ComputeHash(ms.ToArray()).Select(x => x.ToString("X2")));
                }
                var existFile = await _fileServicer.GetFileByMd5Async(md5);
                if (existFile != null)
                {
                    existFile.OwnerId = string.Empty;
                    model = existFile;
                }
                else
                {
                    var path = _hostEnvironment.WebRootPath;
                    var uploadPath = _configuration["AppSettings:ImgUploadPath"]; //避免路径敏感，使用"/"
                    var fullPath = Path.GetFullPath(Path.Combine(path, uploadPath));
                    var filename = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{Path.GetExtension(file.FileName)}";
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    System.IO.File.WriteAllBytes(Path.Combine(fullPath, filename), ms.GetBuffer());
                    model.Name = filename;
                    model.Path = $"/{uploadPath}";
                    model.Md5 = md5;
                }
            }
            model.Id = (await _fileServicer.AddFileAsync(model));
            return model;

        }

        /// <summary>
        /// 此方法将文件移动到删除目录
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Del(string id, string ownerId)
        {
            var existFile = await _fileServicer.GetFileAsync(id);
            if(existFile != null)
            {
                var isMultipleOwner = await _fileServicer.IsMultipleOwnerAsync(id, ownerId);
                var result = await _fileServicer.DelFileAsync(existFile.Id);
                if (!isMultipleOwner && result)
                {
                    var filename = existFile.Name;
                    var path = _hostEnvironment.WebRootPath;
                    var uploadPath = _configuration["AppSettings:ImgUploadPath"];
                    var fullPath = Path.GetFullPath(Path.Combine(path, uploadPath));
                    var delPaht = Path.Combine(fullPath, "del");
                    if (!Directory.Exists(delPaht))
                    {
                        Directory.CreateDirectory(delPaht);
                    }
                    System.IO.File.Copy(Path.Combine(fullPath, filename), Path.Combine(delPaht, filename));
                    System.IO.File.Delete(Path.Combine(fullPath, filename));
                }
                return true;
            }
            return false;
        }
    }
}