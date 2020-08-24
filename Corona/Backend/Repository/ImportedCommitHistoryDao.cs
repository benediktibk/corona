using System;

namespace Backend.Repository {
    public class ImportedCommitHistoryDao {
        public int Id { get; set; }
        public DateTime ImportTimestamp { get; set; }
        public string CommitHash { get; set; }
    }
}
