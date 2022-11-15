using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.UseCase
{
    public class GetProjByStrUseCase
    {
        /// <summary>
        /// テキストからProjectを検索して返却
        /// </summary>
        /// <returns></returns>
        public Project Execute(ObservableCollection<Project> projects, string projectName)
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
    }
}
