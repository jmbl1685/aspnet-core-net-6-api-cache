using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using CacheSample.WebApi.Cache;

namespace CacheSample.WebApi.Attributes
{

    [AttributeUsage(AttributeTargets.All)]
    public class CacheAttribute : Attribute, IActionFilter
    {
        const string cacheIndx = "cache-sample:";

        private ICacheProvider? cacheProvider = null;

        private bool status;
        private int duration;

        /// <summary>
        /// Cache Configuratiopn
        /// </summary>
        /// <param name="Status">If the status is TRUE the cache is enable, otherwise will be disable.</param>
        /// <param name="Duration">Expiry time/duration of the data. Duration time: Minutes</param>
        public CacheAttribute(bool Status = true, int Duration = 0, string Provider = "Redis")
        {
            status = Status;
            duration = Duration;

            if (Provider == "Redis" && cacheProvider is null)
            {
                cacheProvider = CacheSelector.GetRedis();
            }

            if (Provider == "Memory" && cacheProvider is null)
            {
                cacheProvider = CacheSelector.GetMemory();
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var currentResponseTest = context.HttpContext.Response.Body;
            var currentResponse = context.Result;
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;

            Task.Run(async () =>
            {
                try
                {
                    var key = $"{cacheIndx}:{path}_{method}";
                    var okResult = currentResponse as dynamic;
                    var data = okResult?.Value;

                    if (status && data is not null)
                    {
                        await cacheProvider?.Add(key, data, duration);
                    }
                }
                catch (ArgumentException err)
                {
                    throw new ArgumentException(err.Message);
                }
            }).Wait();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Task.Run(async () =>
            {
                try
                {
                    var path = context.HttpContext.Request.Path;
                    var method = context.HttpContext.Request.Method;
                    var key = $"{cacheIndx}:{path}_{method}";

                    context.HttpContext.Items["CacheStatus"] = status;

                    if (status)
                    {
                        var cacheInfo = await cacheProvider.Get<object>(key);

                        if (cacheInfo is not null)
                        {
                            var json = JsonConvert.SerializeObject(cacheInfo);

                            context.Result = new ContentResult()
                            {
                                StatusCode = (int)System.Net.HttpStatusCode.OK,
                                Content = json,
                                ContentType = "application/json",
                            };
                        }
                    }
                }
                catch (ArgumentException err)
                {
                    throw new ArgumentException(err.Message);
                }
            }).Wait();
        }

    }
}


