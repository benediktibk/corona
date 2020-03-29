using Backend.Repository;

namespace Backend.DependencyInjection
{
    public class Container
    {
        private readonly StructureMap.Container _container;

        public Container() {
            _container = new StructureMap.Container(x => {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

                x.For<IDatabase>().Use<Database>();
                x.For<IUnitOfWork>().Use<UnitOfWork>();
                x.ForConcreteType<UnitOfWork>().Configure.Ctor<string>("databaseConnectionString").Is(connectionString);
                x.For<ICountryRepository>().Use<CountryRepository>();
                x.For<ICountryDetailedRepository>().Use<CountryDetailedRepository>();
            });
        }

        public T GetInstance<T>() {
            return _container.GetInstance<T>();
        }
    }
}
