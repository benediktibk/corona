using System.Collections.Generic;

namespace Backend.Repository {
    public class CsvFile {
        private readonly List<CsvFileLine> _lines;
        private readonly Dictionary<string, int> _headers;

        public CsvFile() {
            _lines = new List<CsvFileLine>();
            _headers = new Dictionary<string, int>();
        }

        public CsvFile(Dictionary<string, int> headers, List<CsvFileLine> lines) {
            _lines = lines;
            _headers = headers;
        }

        public IReadOnlyList<CsvFileLine> Lines => _lines;

        public bool TryGetColumnIndexOfHeader(string header, out int index) {
            return _headers.TryGetValue(header, out index);
        }
    }
}
