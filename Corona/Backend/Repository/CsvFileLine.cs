using System.Collections.Generic;

namespace Backend.Repository {
    public class CsvFileLine {
        private readonly List<string> _values;

        public string GetValue(int index) {
            return _values[index];
        }
    }
}
