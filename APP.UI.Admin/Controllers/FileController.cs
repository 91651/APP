using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using APP.Framework.Services;
using APP.Framework.Services.Models;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IFileServicer _fileServicer;

        public FileController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IFileServicer fileServicer)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _fileServicer = fileServicer;
        }

        [HttpPost, Route("UploadImg")]
        [return: NotNull]
        public ActionResult<ResultModel<FileModel>> UploadImg(IFormFile file)
        {
            if (file != null)
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
                    var existFile = _fileServicer.GetFileByMd5(md5);
                    if (existFile != null)
                    {
                        existFile.OwnerId = string.Empty;
                        model = existFile;
                    }
                    else
                    {
                        var path = _hostingEnvironment.WebRootPath;
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
                model.Id = _fileServicer.AddFile(model);
                return new ResultModel<FileModel>
                {
                    Status = true,
                    Data = model
                };
            }
            return new ResultModel<FileModel>
            {
                Status = false,
                Message = "图片上传失败"
            };
        }

        /// <summary>
        /// 此方法将文件移动到删除目录
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet, Route("DelImg")]
        [return: NotNull]
        public ActionResult<ResultModel> DelImg(string id, string ownerId)
        {
            var existFile = _fileServicer.GetFile(id);
            if(existFile != null)
            {
                var isMultipleOwner = _fileServicer.IsMultipleOwner(id, ownerId);
                if (!isMultipleOwner && _fileServicer.DelFile(existFile.Id).Status == true)
                {
                    var filename = existFile.Name;
                    var path = _hostingEnvironment.WebRootPath;
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
                return new ResultModel
                {
                    Status = true
                };
            }
            return new ResultModel
            {
                Status = false,
                Message = "图片删除失败"
            };
        }
    }
}