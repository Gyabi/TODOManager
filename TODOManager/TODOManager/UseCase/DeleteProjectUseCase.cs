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
    public class DeleteProjectUseCase : IDeleteProjectUseCase
    {
        IProjectRepository projectRepository;
        public DeleteProjectUseCase(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public void Execute(ObservableCollection<Project> projects, string projectName)
        {
            Project target = ProjectHelper.GetProjByStr(projects, projectName);
            projects.Remove(target);

            //永続化
            this.projectRepository.DeleteData(target.projectID.id);
        }
    }
}