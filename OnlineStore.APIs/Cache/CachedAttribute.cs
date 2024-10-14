using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Core.Services.Contract.Cache;
using System.Text;

namespace OnlineStore.APIs.Cache
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _time;

        public CachedAttribute(int time) 
        {
            _time = time;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            string cacheKey = GenereteKey(context.HttpContext.Request);

            var cacheRespone = await cacheService.GetCacheKeyAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheRespone))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheRespone,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executeContext = await next();

            if(executeContext.Result is OkObjectResult respone)
            {
                await cacheService.SetCahceKeyAsync(cacheKey, respone, TimeSpan.FromSeconds(_time));
            }
        }

        public string GenereteKey(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(K => K.Key))
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();
        }
    }
}
