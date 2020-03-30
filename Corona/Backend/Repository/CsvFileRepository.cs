using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Backend.Repository
{
    public class CsvFileRepository : ICsvFileRepository
    {
        public List<Dictionary<string, string>> ReadFile(string path) {
            var result = new List<Dictionary<string, string>>();

            using (var file = File.OpenText(path)) {
                var headerLine = file.ReadLine();
                var columnNames = headerLine.Split(',');

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
                    var lineResult = new Dictionary<string, string>();

                    for (var i = 0; i < Math.Min(values.Length, columnNames.Length); ++i) {
                        lineResult.Add(columnNames[i], values[i]);
                    }

                    result.Add(lineResult);
                }
            }

            return result;
        }

        public List<string> ListAllCsvFilesIn(string path) {
            return Directory.GetFiles(path, "*.csv").ToList();
        }
    }
}
