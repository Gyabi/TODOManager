using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class DeleteTodoUseCase : IDeleteTodoUseCase
    {
        public DeleteTodoUseCase()
        {
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, TodoItemID targetId)
        {
            int index = TodoItemHelper.FindTodoIndexByID(todoItems.ToList(), targetId);
            todoItems.Remove(todoItems[index]);
        }
    }
}