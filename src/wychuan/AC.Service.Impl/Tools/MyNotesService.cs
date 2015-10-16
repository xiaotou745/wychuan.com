using System;
using System.Collections.Generic;
using AC.Dao.Tools;
using AC.Service.DTO.Tools;
using AC.Service.Tools;
using AC.Util;

namespace AC.Service.Impl.Tools
{
    public class MyNotesService : IMyNotesService
    {
        private readonly MyNotesDao myNotesDao;

        public MyNotesService()
        {
            myNotesDao = new MyNotesDao();
        }

        public int Create(MyNotesDTO myNotesDTO)
        {
            AssertUtils.ArgumentNotNull(myNotesDTO, "myNotes");

            return myNotesDao.Insert(myNotesDTO);
        }

        public IList<MyNotesDTO> Query(MyNotesQueryInfo queryInfo)
        {
            if (queryInfo.CreateTimeRange != null)
            {
                if (queryInfo.CreateTimeRange.StartTime.HasValue)
                {
                    queryInfo.CreateTimeRange.StartTime = queryInfo.CreateTimeRange.StartTime.Value.Date;
                }
                if (queryInfo.CreateTimeRange.OverTime.HasValue)
                {
                    queryInfo.CreateTimeRange.OverTime = queryInfo.CreateTimeRange.OverTime.Value.Date.AddDays(1);
                }
            }
            return myNotesDao.Query(queryInfo);
        }

        public void Remove(int id)
        {
            AssertUtils.Greater(id, 0);

            myNotesDao.Delete(id);
        }
    }
}