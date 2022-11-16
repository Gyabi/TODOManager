using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.UseCase.interfaces
{
    public interface IAddTodoUseCase
    {
        public TodoItem Execute(ObservableCollection<Project> projects, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail);
    }
}
