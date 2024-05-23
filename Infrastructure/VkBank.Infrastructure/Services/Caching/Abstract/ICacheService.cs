﻿namespace VkBank.Infrastructure.Services.Caching.Abstract
{
    public interface ICacheService
    {
        public T? GetCache<T>(string key, string source);
        public void AddCache(string key, object value, TimeSpan duration, string source);
        public void RemoveCache(string key, string source);
    }
}
