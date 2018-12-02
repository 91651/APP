using System;
using System.IO;
using APP.Framework.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public IConfiguration Configuration { get; }

        public FileController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        [HttpPost, Route("UploadImg")]
        public ActionResult<ResultModel<FileModel>> UploadImg(IFormFile file)
        {
            if (file != null)
            {
                var path = _hostingEnvironment.WebRootPath;
                var uploadPath = Configuration["AppSettings:ImgUploadPath"]; //避免路径敏感，使用"/"
                var fullPath = Path.GetFullPath(Path.Combine(path, uploadPath));
                var filename = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{Path.GetExtension(file.FileName)}";
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                using (FileStream fs = System.IO.File.Create(Path.Combine(fullPath, filename)))
                {
                    // 复制文件
                    file.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
                return new ResultModel<FileModel>
                {
                    Status = true,
                    Data = new FileModel { Name = filename, Path = uploadPath }
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
        public ActionResult<ResultModel> DelImg(string filename)
        {
            var path = _hostingEnvironment.WebRootPath;
            var uploadPath = Configuration["AppSettings:ImgUploadPath"];
            var fullPath = Path.GetFullPath(Path.Combine(path, uploadPath));
            var delPaht = Path.Combine(fullPath, "del");
            if (!Directory.Exists(delPaht))
            {
                Directory.CreateDirectory(delPaht);
            }
            System.IO.File.Copy(Path.Combine(fullPath, filename), Path.Combine(delPaht, filename));
            System.IO.File.Delete(Path.Combine(fullPath, filename));
            return new ResultModel
            {
                Status = true
            };
        }
    }
}