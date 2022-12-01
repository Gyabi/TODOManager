using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class ChangeChildTodoStatusUseCase : IChangeChildTodoStatusUseCase
    {
        ITodoRepository todoRepository;
        public ChangeChildTodoStatusUseCase(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, TodoItemID id, int row)
        {
            int index = TodoItemHelper.FindTodoIndexByID(todoItems.ToList(), id);
            todoItems[index] = TodoItem.ChangeChildStatus(todoItems[index], row);

            this.todoRepository.UpdateData(todoItems[index].id, todoItems[index], index);
        }
    }
}