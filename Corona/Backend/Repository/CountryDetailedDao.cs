namespace Backend.Repository
{
    public class CountryDetailedDao
    {
        public int Id { get; set; }
        public CountryType CountryId { get; set; }
        public int Inhabitants { get; set; }
        public int IcuBeds { get; set; }
        public double MoratilityRatePerOneMillionPerDay { get; set; }
    }
}
