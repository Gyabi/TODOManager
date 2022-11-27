using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class ReadTodoUseCase : IReadTodoUseCase
    {
        ITodoRepository todoRepository;
        public ReadTodoUseCase(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public ObservableCollection<TodoItem> Execute()
        {
            return new ObservableCollection<TodoItem>(this.todoRepository.ReadTodoItems());
        }
    }
}