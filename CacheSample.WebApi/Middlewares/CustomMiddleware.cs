using System.Globalization;

namespace CacheSample.WebApi.Middlewares
{
    public static class CustomMiddleware
    {
        public static void UseCustomMiddleware(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path;
                var method = context.Request.Method;
                var contentType = context.Request.Headers;
                await next(context);
            });
        }
    }

}
