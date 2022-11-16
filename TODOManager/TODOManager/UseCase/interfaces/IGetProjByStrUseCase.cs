using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.UseCase.interfaces
{
    public interface IGetProjByStrUseCase
    {
        public Project Execute(ObservableCollection<Project> projects, string projectName);

    }
}
