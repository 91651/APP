using APP.Framework.Captcha;

namespace APP.Business.Services
{
    public interface IAuthService
    {
        CaptchaModel CaptchaGenerate();
    }
}