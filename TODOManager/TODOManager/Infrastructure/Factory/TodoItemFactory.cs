using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;

namespace TODOManager.Infrastructure.Factory
{
    public class TodoItemFactory : ITodoItemFactory
    {
        public TodoItemID CreateTodoItemID()
        {
            string id = Guid.NewGuid().ToString();
            return new TodoItemID(id);
        }

    }
}
