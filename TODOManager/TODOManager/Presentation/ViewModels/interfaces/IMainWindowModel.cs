using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Presentation.ViewModels.interfaces
{
    public interface IMainWindowModel
    {
        public ReactiveCollection<TodoItem> GetTodoItems();
        public void AddTodoItem(TodoItem todoItem);

    }
}
