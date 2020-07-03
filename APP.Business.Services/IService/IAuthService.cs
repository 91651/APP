using APP.Framework.Captcha;
using System.Threading.Tasks;

namespace APP.Business.Services
{
    public interface IAuthService
    {
        Task<CaptchaModel> CaptchaGenerateAsync();
        Task<string> CaptchaVerifyAsync(Point point);
    }
}