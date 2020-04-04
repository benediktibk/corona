using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.OutputCache.Core.Cache;

namespace CoronaSpreadViewer
{
    public class ServerSideCache : IApiOutputCache
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, object> _cache;

        public ServerSideCache() {
            _cache = new Dictionary<string, object>();
        }

        public IEnumerable<string> AllKeys => _cache.Select(x => x.Key);

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null) {
            _logger.Debug($"adding value for key {key}");
            _cache.Add(key, o);
        }

        public bool Contains(string key) {
            _logger.Debug($"checking if key {key} is available");
            return _cache.ContainsKey(key);
        }

        public T Get<T>(string key) where T : class {
            _logger.Debug($"fetching value for key {key}");
            return _cache[key] as T;
        }

        public object Get(string key) {
            _logger.Debug($"fetching value for key {key}");
            return _cache[key];
        }

        public void Remove(string key) {
            _logger.Debug($"removing value for key {key}");
            _cache.Remove(key);
        }

        public void RemoveStartsWith(string key) {
            _logger.Debug($"removing values for keys which start with {key}");
            var affectedKeys = _cache.Select(x => x.Key).Where(x => x.StartsWith(key));

            foreach (var affectedKey in affectedKeys) {
                _cache.Remove(affectedKey);
            }
        }
    }
}