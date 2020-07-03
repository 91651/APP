using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using APP.Framework.Captcha;
using Microsoft.Extensions.Caching.Memory;

namespace APP.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;


        public AuthService(IMapper mapper, IMemoryCache memoryCache, IUserRepository userRepository)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public CaptchaModel CaptchaGenerate()
        {
            var captcha = new ImageCaptcha().Generate();
            _memoryCache.Set(nameof(ImageCaptcha), captcha.Point);
            captcha.Point.X = default;
            captcha.Point.Y = default;
            return captcha;
        }
    }
}
