namespace Backend.Repository
{
    public class CountryInhabitantsDao
    {
        public int Id { get; set; }
        public CountryType CountryId { get; set; }
        public int Inhabitants { get; set; }
    }
}
