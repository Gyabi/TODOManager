using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class AddProjectUseCase : IAddProjectUseCase
    {
        IProjectFactory projectFactory;
        public AddProjectUseCase(IProjectFactory projectFactory)
        {
            this.projectFactory = projectFactory;
        }

        public void Execute(ObservableCollection<Project> projects, string projectName)
        {
            projects.Add(new Project(projectName, this.projectFactory.CreateProjectID()));
        }
    }
}