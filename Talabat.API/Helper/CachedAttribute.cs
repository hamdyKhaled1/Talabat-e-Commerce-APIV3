using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Service;

namespace Talabat.API.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;
        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey=GenarateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await CacheService.GetCachedResponseAsync(cacheKey);
            if(!string.IsNullOrEmpty(cachedResponse) )
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= contentResult;
            }
            var executeEndPoint = await next();
            if(executeEndPoint.Result is OkObjectResult okObjectResult)
            {
                await CacheService.CacheResponseAsync(cacheKey, okObjectResult.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }

        }

        private string GenarateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach(var (key, value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
