using Backend;
using System;
using System.Collections.Generic;
using WebApi.OutputCache.Core.Cache;

namespace CoronaSpreadViewer
{
    public class ServerSideCacheWrapper : IApiOutputCache
    {
        private readonly IServerSideCache _serverSideCache;

        public ServerSideCacheWrapper(IServerSideCache serverSideCache) {
            _serverSideCache = serverSideCache;
        }

        public IEnumerable<string> AllKeys => _serverSideCache.AllKeys;

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null) {
            _serverSideCache.Add(key, o);
        }

        public bool Contains(string key) {
            return _serverSideCache.Contains(key);
        }

        public T Get<T>(string key) where T : class {
            return _serverSideCache.Get<T>(key);
        }

        public object Get(string key) {
            return _serverSideCache.Get(key);
        }

        public void Remove(string key) {
            _serverSideCache.Remove(key);
        }

        public void RemoveStartsWith(string key) {
            _serverSideCache.RemoveStartsWith(key);
        }
    }
}