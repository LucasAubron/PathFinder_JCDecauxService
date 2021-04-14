using System;
using System.Runtime.Caching;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace WPS
{
    class ProxyCache<T> where T: class
    {
        private double dt_default = 60; // in seconds
        private MemoryCache memoryCache = new MemoryCache("very cool cache");

        public T Get(String id)
        {
            return this.Get(id, this.dt_default);
        }
        public T Get(String id, double dt_seconds)
        {
            return this.Get(id, DateTimeOffset.Now.AddSeconds(dt_seconds));
        }

        public T Get(String id, DateTimeOffset dt)
        {
            if (this.memoryCache.GetCacheItem(id) == null) {
                Object[] parameters = { id };
                this.memoryCache.Add(id, (T)Activator.CreateInstance(typeof(T), parameters), dt);
            }
            return (T) this.memoryCache.GetCacheItem(id).Value;
        }

    }
}
