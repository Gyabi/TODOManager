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
    public class SortTodoUseCase : ISortTodoUseCase
    {
        ITodoRepository todoRepository;
        public SortTodoUseCase(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, int from, int to)
        {
            todoItems.Move(from, to);

            //すべてのTODOに対してindexを更新
            foreach(var (todo, index) in todoItems.Select((todo, index) => (todo, index)))
            {
                this.todoRepository.UpdateDataOnlyIndex(todo.id, index);
            }
        }
    }
}