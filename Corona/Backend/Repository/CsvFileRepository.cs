using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Backend.Repository {
    public class CsvFileRepository : ICsvFileRepository {
        public CsvFile ReadFile(string path) {
            var headers = new Dictionary<string, int>();
            var lines = new List<CsvFileLine>();

            using (var file = File.OpenText(path)) {
                var headerLine = file.ReadLine();
                var columnNames = headerLine.Split(',');

                for (var i = 0; i < columnNames.Length; ++i) {
                    headers.Add(columnNames[i], i);
                }

                while (!file.EndOfStream) {
                    var line = file.ReadLine();

                    var matches = Regex.Matches(line, "\"([^\",]*,[^\"]*)\"");

                    for (var i = 0; i < matches.Count; ++i) {
                        var match = matches[i].Groups[1].Value;
                        var matchReplacement = Regex.Replace(match, ",", "");
                        match = match.Replace("(", "\\(");
                        match = match.Replace(")", "\\)");
                        line = Regex.Replace(line, $"\"{match}\"", matchReplacement);
                    }

                    var values = line.Split(',');
                    lines.Add(new CsvFileLine(values));
                }
            }

            return new CsvFile(headers, lines);
        }

        public List<string> ListAllCsvFilesIn(string path) {
            return Directory.GetFiles(path, "*.csv").ToList();
        }
    }
}
