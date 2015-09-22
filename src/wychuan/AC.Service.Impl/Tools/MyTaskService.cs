using System;
using System.Collections.Generic;
using AC.Dao.Tools;
using AC.Service.DTO.Tools;
using AC.Service.Tools;
using AC.Util;

namespace AC.Service.Impl.Tools
{
    public class MyTaskService : IMyTaskService
    {
        private readonly MyTaskDao myTaskDao;

        public MyTaskService()
        {
            myTaskDao = new MyTaskDao();
        }

        public int Create(MyTaskDTO myTask)
        {
            AssertUtils.ArgumentNotNull(myTask, "myTask");
            AssertUtils.ArgumentNotNull(myTask.Content, "myTask.Content");

            return myTaskDao.Insert(myTask);
        }

        public void Modify(MyTaskDTO myTask)
        {
            AssertUtils.ArgumentNotNull(myTask, "myTask");
            AssertUtils.ArgumentNotNull(myTask.Content, "myTask.Content");

            myTaskDao.Update(myTask);
        }

        public IList<MyTaskDTO> GetMyTasks(MyTaskQueryInfo queryInfo)
        {
            return myTaskDao.GetMyTask(queryInfo);
        }
        
        public void ModifyStatus(int id, int status)
        {
            myTaskDao.UpdateStatus(id, status);
        }

        public void Hide(int id)
        {
            myTaskDao.Hide(id);
        }

        public void Remove(int id)
        {
            myTaskDao.Delete(id);
        }
    }
}