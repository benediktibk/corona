﻿using System;
using System.Collections.Generic;

namespace Backend.Repository {
    public interface IInfectionSpreadDataPointRepository {
        void DeleteAll(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints);
        List<InfectionSpreadDataPointDao> GetAllForCountryOrderedByDate(IUnitOfWork unitOfWork, CountryType country);
        InfectionSpreadDataPointDao GetMostRecentDataPoint(IUnitOfWork unitOfWork, CountryType country);
        InfectionSpreadDataPointDao GetLastDataPointBefore(IUnitOfWork unitOfWork, CountryType country, DateTime dateTime);
        DateTime GetMostRecentDateTime(IUnitOfWork unitOfWork);
    }
}