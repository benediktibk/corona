using System;

namespace Backend.Repository {
    public class InfectionSpreadDataPointDao {
        public int Id { get; set; }
        public CountryType CountryId { get; set; }
        public DateTime Date { get; set; }
        public int InfectedTotal { get; set; }
        public int DeathsTotal { get; set; }
        public int RecoveredTotal { get; set; }
    }
}
