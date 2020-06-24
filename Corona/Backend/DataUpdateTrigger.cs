using Backend;
using Backend.Service;
using NLog;
using System;
using System.Timers;

namespace Backend {
    public class DataUpdateTrigger : IDataUpdateTrigger {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDataReimportService _dataReimportService;
        private readonly IDataUpdateTimerService _dataUpdateTimerService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IServerSideCache _serverSideCache;
        private readonly Timer _timer;

        public DataUpdateTrigger(IDataReimportService dataReimportService, IDataUpdateTimerService dataUpdateTimerService, IUnitOfWorkFactory unitOfWorkFactory, IServerSideCache serverSideCache) {
            _dataReimportService = dataReimportService;
            _dataUpdateTimerService = dataUpdateTimerService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _serverSideCache = serverSideCache;
            _timer = new Timer();
            _timer.Elapsed += UpdateData;
            _timer.AutoReset = false;
        }

        public void Start() {
            _timer.Interval = _dataUpdateTimerService.CalculateIntervalInMilliseconds(DateTime.Now);
            _timer.Start();
        }

        private void UpdateData(object sender, ElapsedEventArgs eventArgs) {
            try {
                _logger.Info("triggering update of data");

                using (var unitOfWork = _unitOfWorkFactory.Create()) {
                    unitOfWork.BeginDatabaseTransaction();
                    _dataReimportService.ReimportAll(unitOfWork);
                    unitOfWork.CommitDatabaseTransaction();
                }

                _logger.Info("successfully updated data, invalidating the server side cache");
                _serverSideCache.Invalidate();
            }
            catch (Exception e) {
                _logger.Error(e, "an error occured during the automatic data update");
            }
            finally {
                Start();
            }
        }
    }
}