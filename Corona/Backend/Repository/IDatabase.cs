﻿using System.Data.SqlClient;

namespace Backend.Repository
{
    public interface IDatabase
    {
        void Initialize(IUnitOfWork unitOfWork);
    }
}