using System;
using System.Collections.Generic;
using WebApi.OutputCache.Core.Cache;

namespace CoronaSpreadViewer
{
    public class ServerSideCache : IApiOutputCache
    {
        public IEnumerable<string> AllKeys => throw new NotImplementedException();

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null) {
            throw new NotImplementedException();
        }

        public bool Contains(string key) {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : class {
            throw new NotImplementedException();
        }

        public object Get(string key) {
            throw new NotImplementedException();
        }

        public void Remove(string key) {
            throw new NotImplementedException();
        }

        public void RemoveStartsWith(string key) {
            throw new NotImplementedException();
        }
    }
}