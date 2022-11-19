using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Helpers
{
    public class TodoItemHelper
    {
        public static int FindTodoIndexByID(List<TodoItem> todoItems, TodoItemID id)
        {
            foreach(var (value, index) in todoItems.Select((value, index) => (value, index)))
            {
                if (value.id == id) return index;
            }

            throw new CannotUnloadAppDomainException();
        }

        public static TodoItem GetTodoByID(List<TodoItem> todoItems, TodoItemID id)
        {
            foreach (var value in todoItems)
            {
                if (value.id == id) return value;
            }

            throw new CannotUnloadAppDomainException();
        }
    }
}
