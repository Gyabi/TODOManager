using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Domain.DomainService.Repository
{
    public interface ITodoRepository
    {
        public List<TodoItem> ReadTodoItems();
        public void InsertData(TodoItem todoItem, int index);
        public void UpdateData(TodoItemID id, TodoItem todoItem, int index);
        public void DeleteData(TodoItemID id);
        public void UpdateDataOnlyIndex(TodoItemID id, int index);
    }
}
