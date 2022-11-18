using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Helpers
{
    public static class ProjectHelper
    {
        /// <summary>
        /// テキストからProjectを検索して返却
        /// </summary>
        /// <returns></returns>
        public static Project GetProjByStr(ObservableCollection<Project> projects, string projectName)
        {
            Project outProject = null;
            foreach (Project _project in projects)
            {
                if (_project.projectName == projectName)
                {
                    outProject = _project;
                }
            }
            return outProject;
        }

        /// <summary>
        /// プロジェクトIDからプロジェクト名を検索して返却
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public static string GetProjNameByID(ObservableCollection<Project> projects, ProjectID projectID)
        {
            string projectName = string.Empty;
            foreach(Project _project in projects)
            {
                if(_project.projectID == projectID)
                {
                    projectName = _project.projectName;
                }
            }
            return projectName;
        }

    }
}
