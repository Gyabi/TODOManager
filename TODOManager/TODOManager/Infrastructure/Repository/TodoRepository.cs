using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Presentation.ViewModels.Contents;

namespace TODOManager.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary>
        /// DBから保存済みアイテムを呼び出す
        /// </summary>
        /// <returns></returns>
        public List<TodoItem> ReadTodoItems()
        {
            Detail detailSample = new Detail("testtesttesttesttesttesttesttesttesttesttest\ntestestsetestsetest");
            var todoItems = new List<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.HIGH, detailSample));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.MEDIUM, new Detail("detail\ntest\n>aaaaaaaaa")));
            todoItems.Add(new TodoItem("test3", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.MEDIUM, new Detail("detail")));

            return todoItems;
        }
    }
}
