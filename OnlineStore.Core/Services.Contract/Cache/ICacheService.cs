using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract.Cache
{
    public interface ICacheService
    {
       Task<string> GetCacheKeyAsync(string key);
       Task SetCahceKeyAsync(string key, object response, TimeSpan expireDate);
    }
}
