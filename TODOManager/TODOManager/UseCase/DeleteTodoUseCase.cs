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
    public class DeleteTodoUseCase : IDeleteTodoUseCase
    {
        ITodoRepository todoRepository;
        public DeleteTodoUseCase(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, TodoItemID targetId)
        {
            int index = TodoItemHelper.FindTodoIndexByID(todoItems.ToList(), targetId);
            
            this.todoRepository.DeleteData(todoItems[index].id);
            todoItems.Remove(todoItems[index]);

        }
    }
}