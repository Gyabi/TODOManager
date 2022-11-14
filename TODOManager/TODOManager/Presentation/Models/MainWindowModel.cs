using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.Presentation.ViewModels.interfaces;

namespace TODOManager.Presentation.Models
{
    /// <summary>
    /// MainWindowに対するModelクラス
    /// ApplicationServise層へ処理を委託する
    /// </summary>
    public class MainWindowModel : IMainWindowModel
    {
        public ReactiveCollection<TodoItem> GetTodoItems()
        {
            ReactiveCollection<TodoItem> todoItems = new ReactiveCollection<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.HIGH, new Detail("detail"), null, new List<TodoItem>()));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));
            todoItems.Add(new TodoItem("test3", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));

            return todoItems;
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            System.Diagnostics.Debug.WriteLine("add todo item");
        }
    }
}
