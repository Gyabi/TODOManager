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
    public class SortTodoUseCase : ISortTodoUseCase
    {
        public SortTodoUseCase()
        {
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, int from, int to)
        {
            todoItems.Move(from, to);
        }
    }
}