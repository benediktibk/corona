using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
    public class CsvFileLine {
        private readonly List<string> _values;

        public CsvFileLine(IReadOnlyList<string> values) {
            _values = values.ToList();
        }

        public string GetValue(int index) {
            return _values[index];
        }
    }
}
