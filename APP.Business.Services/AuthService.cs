using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using APP.Framework.Captcha;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

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

        public async Task<CaptchaModel> CaptchaGenerateAsync()
        {
            var captcha = new ImageCaptcha().Generate();
            var point = JsonConvert.DeserializeObject<Point>(JsonConvert.SerializeObject(captcha.Point));
            _memoryCache.Set(nameof(ImageCaptcha), point, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) });
            captcha.Point.X = default;
            captcha.Point.Y = default;
            return await Task.FromResult(captcha);
        }

        public async Task<string> CaptchaVerifyAsync(Point point)
        {
            var p = _memoryCache.Get<Point>(nameof(ImageCaptcha));
            _memoryCache.Remove(nameof(ImageCaptcha));
            var code = ImageCaptcha.CaptchaVerify(p, point);
            if (!string.IsNullOrWhiteSpace(code))
            {
                _memoryCache.Set(code, true);
                return code;
            }
            return await Task.FromResult(string.Empty);
        }
    }
}
