using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class ReadProjectUseCase : IReadProjectUseCase
    {
        IProjectRepository projectRepository;
        public ReadProjectUseCase(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public ObservableCollection<Project> Execute()
        {
            return new ObservableCollection<Project>(this.projectRepository.ReadProjects());
        }
    }
}