using System.Collections.Generic;

namespace Backend {
    public interface IServerSideCache {
        IEnumerable<string> AllKeys { get; }

        void Add(string key, object o);
        bool Contains(string key);
        T Get<T>(string key) where T : class;
        object Get(string key);
        void Remove(string key);
        void RemoveStartsWith(string key);
        void Invalidate();
    }
}