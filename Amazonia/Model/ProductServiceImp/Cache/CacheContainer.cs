using Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.Cache
{
    public class CacheContainer
    {
        private static CacheContainer container = null;
        private MemoryCache cache;
        private List<string> cacheEntries;

        private CacheContainer()
        {
            cache = new MemoryCache("cache");
            cacheEntries = new List<string>();
        }

        public static CacheContainer GetCacheContainer()
        {
            if (container == null)
            {
                container = new CacheContainer();
            }
            return container;
        }

        public void AddToCache(String entrie, ProductBlock result)
        {
            CacheItem item = new CacheItem(entrie, result);
            CacheItemPolicy itemPolicy = new CacheItemPolicy();

            if (cache.GetCount() < 5)
            {
                cacheEntries.Add(entrie);
                cache.Add(item, itemPolicy);
            }
            else
            {
                String firstItem = cacheEntries[0];
                cacheEntries.Remove(firstItem);
                cacheEntries.Add(entrie);

                cache.Remove(firstItem);
                cache.Add(item, itemPolicy);
            }
        }

        public ProductBlock GetEntrie(String entrie)
        {
            return (ProductBlock)cache.GetCacheItem(entrie).Value;
        }

        public bool IsInCache(String entrie)
        {
            return cache.Contains(entrie);
        }

    }
}
