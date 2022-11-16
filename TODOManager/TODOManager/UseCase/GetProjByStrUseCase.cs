using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class GetProjByStrUseCase : IGetProjByStrUseCase
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
