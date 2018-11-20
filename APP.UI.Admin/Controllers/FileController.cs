using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        [HttpPost, Route("UploadImg")]
        [FromHeader()]
        public IActionResult UploadImg(IFormFile file)
        {
            if (Request.Form.Files.Count > 0)
            {
                return Ok();
            }
            return View();
        }
    }
}