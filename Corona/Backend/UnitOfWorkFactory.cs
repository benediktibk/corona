namespace Backend
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly Settings _settings;

        public UnitOfWorkFactory(Settings settings) {
            _settings = settings;
        }

        public IUnitOfWork Create() {
            return new UnitOfWork(_settings);
        }
    }
}
