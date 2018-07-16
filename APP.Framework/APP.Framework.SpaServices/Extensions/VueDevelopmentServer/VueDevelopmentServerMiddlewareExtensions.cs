using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;

namespace APP.Framework.SpaServices.VueDevelopmentServer
{
    public static class VueDevelopmentServerMiddlewareExtensions
    {
        public static void UseVueDevelopmentServer(
            this ISpaBuilder spaBuilder,
            string npmScript)
        {
            if (spaBuilder == null)
            {
                throw new ArgumentNullException(nameof(spaBuilder));
            }

            var spaOptions = spaBuilder.Options;

            if (string.IsNullOrEmpty(spaOptions.SourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(UseVueDevelopmentServer)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            VueDevelopmentServerMiddleware.Attach(spaBuilder, npmScript);
        }
    }
}
