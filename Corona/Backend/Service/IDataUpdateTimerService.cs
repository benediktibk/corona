using System;

namespace Backend.Service {
    public interface IDataUpdateTimerService {
        double CalculateIntervalInMilliseconds(DateTime now);
    }
}