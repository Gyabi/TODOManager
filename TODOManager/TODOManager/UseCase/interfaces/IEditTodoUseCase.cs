using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.UseCase.interfaces
{
    public interface IEditTodoUseCase
    {
        public void Execute(TodoItemID targetID, ObservableCollection<TodoItem> todoItems, ObservableCollection<Project> projects, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail);
    }
}
