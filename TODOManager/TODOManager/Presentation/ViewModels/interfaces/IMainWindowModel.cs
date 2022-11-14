using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Presentation.ViewModels.interfaces
{
    public interface IMainWindowModel
    {
        public ObservableCollection<TodoItem> todoItems { get; }
        public void AddTodoItem();

    }
}
