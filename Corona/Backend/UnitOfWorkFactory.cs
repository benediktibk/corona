namespace Backend
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISettings _settings;

        public UnitOfWorkFactory(ISettings settings) {
            _settings = settings;
        }

        public IUnitOfWork Create() {
            return new UnitOfWork(_settings);
        }
    }
}
