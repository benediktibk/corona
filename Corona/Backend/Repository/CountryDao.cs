namespace Backend.Repository
{
    public class CountryDao
    {
        public CountryType Id { get; set; }
        public string Name { get; set; }
        public int Inhabitants { get; set; }
        public int IcuBeds { get; set; }
        public double MoratilityRatePerOneMillionPerDay { get; set; }
    }
}
