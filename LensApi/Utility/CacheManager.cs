using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace Me.JieChen.Lens.Api.Utility;

public sealed class CacheManager
{
    private static int CacheSizeLimit = 1024;
    private static int CacheEntrySizeLimit = 1;
    private static int ExpirationScanIntervalInMinute = 10;
    private static int AbsoluteExpirationInMinute = 7 * 24 * 60;
    private static int SlidingExpirationInMinute = 100 * 60;

    private static CacheManager? instance;

    private readonly static SemaphoreSlim instanceLock = new SemaphoreSlim(1);
    private readonly SemaphoreSlim cacheLock = new SemaphoreSlim(1);
    private readonly IMemoryCache cache;
    private readonly MemoryCacheEntryOptions entryOptions;

    private CacheManager()
    {
        cache = new MemoryCache
        (
            new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(ExpirationScanIntervalInMinute),
                SizeLimit = CacheSizeLimit
            }
        );

        entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpirationInMinute),
            SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationInMinute),
            Size = CacheEntrySizeLimit
        };
    }
    public static CacheManager GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }

        try
        {
            instanceLock.WaitAsync().Wait();
            instance = new CacheManager();
            return instance;
        }
        finally
        {
            instanceLock.Release();
        }
    }

    public async Task<TValue> GetAsync<TKey, TValue>(TKey key, Func<Task<TValue>> callbackIfNotFroundFromCache)
        where TKey : class
        where TValue : class
    {
        if (cache.TryGetValue(key, out TValue value))
        {
            return value;
        }

        try
        {
            await cacheLock.WaitAsync().ConfigureAwait(false);
            TValue newValue = await callbackIfNotFroundFromCache.Invoke().ConfigureAwait(false);
            cache.Set<TValue>(key, newValue, entryOptions);
            return newValue;
        }
        finally
        {
            cacheLock.Release();
        }
    }
}
