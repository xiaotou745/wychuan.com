using System;
using System.Collections.Generic;
using AC.Dao.LiCai;
using AC.Service.DTO.LiCai;
using AC.Service.LiCai;
using AC.Util;

namespace AC.Service.Impl.LiCai
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectDao projectDao;

        public ProjectService()
        {
            projectDao = new ProjectDao();
        }

        public void InitUserProject(int userId)
        {
            projectDao.InitUserProject(userId);
        }

        public int Create(ProjectDTO project)
        {
            AssertUtils.ArgumentNotNull(project, "project");

            return projectDao.Insert(project);
        }

        public IList<ProjectDTO> GetByUserId(int userId)
        {
            return projectDao.GetByUserId(userId);
        }
    }
}