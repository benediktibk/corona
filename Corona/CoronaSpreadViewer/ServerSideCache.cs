using NLog;
using System;
using System.Collections.Generic;
using WebApi.OutputCache.Core.Cache;

namespace CoronaSpreadViewer
{
    public class ServerSideCache : IApiOutputCache
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<string> AllKeys => new List<string>();

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null) {
            _logger.Debug($"adding value for key {key}");
            return;
        }

        public bool Contains(string key) {
            _logger.Debug($"checking if key {key} is available");
            return false;
        }

        public T Get<T>(string key) where T : class {
            _logger.Debug($"fetching value for key {key}");
            throw new KeyNotFoundException();
        }

        public object Get(string key) {
            _logger.Debug($"fetching value for key {key}");
            throw new KeyNotFoundException();
        }

        public void Remove(string key) {
            _logger.Debug($"removing value for key {key}");
            throw new KeyNotFoundException();
        }

        public void RemoveStartsWith(string key) {
            _logger.Debug($"removing values for keys which start with {key}");
            throw new KeyNotFoundException();
        }
    }
}