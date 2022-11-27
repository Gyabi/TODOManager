using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class AddProjectUseCase : IAddProjectUseCase
    {
        IProjectFactory projectFactory;
        IProjectRepository projectRepository;
        public AddProjectUseCase(IProjectFactory projectFactory, IProjectRepository projectRepository)
        {
            this.projectFactory = projectFactory;
            this.projectRepository = projectRepository;
        }

        public void Execute(ObservableCollection<Project> projects, string projectName)
        {
            Project project = new Project(projectName, this.projectFactory.CreateProjectID());
            projects.Add(project);

            //永続化
            this.projectRepository.InsertData(project.projectID.id, project.projectName);
        }
    }
}