namespace Backend.Repository
{
    public class InfectionSpreadDataPointRepository : IInfectionSpreadDataPointRepository
    {
        public void DeleteAll(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand("TRUNCATE TABLE InfectionSpreadDataPoint");
        }

        public void Insert(IUnitOfWork unitOfWork, InfectionSpreadDataPointDao dataPoint) {
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT InfectionSpreadDataPoint
                (
                    [Date],
                    Country,
                    InfectedTotal,
                    DeathsTotal,
                    RecoveredTotal
                )
                VALUES
                (
                    @Date,
                    @Country,
                    @InfectedTotal,
                    @DeathsTotal,
                    @RecoveredTotal
                )", dataPoint);
        }
    }
}
