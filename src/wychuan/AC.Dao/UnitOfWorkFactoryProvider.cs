﻿using AC.Data.ConnString;
using AC.Transaction.Common;

namespace AC.Dao
{
    public class UnitOfWorkFactoryProvider
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            return UnitOfWorkFactory.GetAdoNetUnitOfWork(ConnStringUtil.GetConnectionString("ConnStringOfAchuan"));
        } 
    }
}