using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<TodoItem> todoItems { get; }
        public MainWindowModel()
        {
            List<TodoItem> sample = new List<TodoItem>() { new TodoItem("child", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.NONE, new Detail("detail"), null, new List<TodoItem>()) };
            Detail detailSample = new Detail("testtesttesttesttesttesttesttesttesttesttest\ntestestsetestsetest");
            
            todoItems = new ObservableCollection<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.HIGH, detailSample, null, sample));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));
            todoItems.Add(new TodoItem("test3", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));

        }

        public void AddTodoItem()
        {
            TodoItem addItem = new TodoItem("add", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>());
            this.todoItems.Add(addItem);
        }
    }
}
