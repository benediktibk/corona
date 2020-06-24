using System;
using NLog;

namespace Backend.Service {
    public class DataUpdateTimerService : IDataUpdateTimerService {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public double CalculateIntervalInMilliseconds(DateTime now) {
            var target = now;
            _logger.Info($"starting data update trigger at {target}");

            if (target.Hour >= 6) {
                _logger.Info("it is already past 6, adding one day");
                target = target.AddDays(1);
            }

            target = new DateTime(target.Year, target.Month, target.Day, 6, 0, 0);
            return (target - now).TotalMilliseconds;
        }
    }
}
