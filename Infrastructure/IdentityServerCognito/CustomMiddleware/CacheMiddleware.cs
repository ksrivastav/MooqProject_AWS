using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityServerCognito.CustomMiddleware
{
    public class CacheMiddleware : IMiddleware
    {
        //private readonly RequestDelegate _next;
        IMemoryCache _memoryCache;
        public CacheMiddleware( IMemoryCache memoryCache)
        {            
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
        {
            Console.WriteLine("hello");
            var response = _memoryCache.TryGetValue<GetSecretValueResponse>("IdentityConfigData", out GetSecretValueResponse configResponse);
            Console.WriteLine(configResponse);
            if (!response)
            {
                IdentityCognitoConfigretriever getConfig = new IdentityCognitoConfigretriever();
                var configData = await getConfig.GetConfig();
                _memoryCache.Set<GetSecretValueResponse>("IdentityConfigData",configData);
            }
            var response1 = _memoryCache.TryGetValue<GetSecretValueResponse>("IdentityConfigData", out GetSecretValueResponse configResponse1);
            Console.WriteLine(response1.ToString());
            Console.WriteLine(configResponse1);
            await _next(httpContext);
        }
    }

    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheMiddleware>();
        }
    }

}
