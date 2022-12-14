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
        //Todoアイテムのリスト
        public ObservableCollection<TodoItem> todoItems { get; }
        //プロジェクトを示すリスト
        public ObservableCollection<Project> projects { get; set; }
        //プライオリティを示すリスト
        public ObservableCollection<Priority> priorities { get; set; }
        public void AddTodoItem(string itemName, string projectID, bool useDeadLine, DateTime deadLine, string priority, string detail);
        public void ChangeChildItemStatus(TodoItemID id, int row);
        public void DeleteTodoItem(TodoItemID id);
        public void SortTodoItem(int from, int to);
        public TodoItem GetTodoItemData(TodoItemID id);
        public void EditTodoItem(TodoItemID targetID, string itemName, string projectID, bool useDeadLine, DateTime deadLine, string priority, string detail);

        public void DeleteProject(string projectName);
        public void AddProject(string projectName);
    }
}
