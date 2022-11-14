using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Reactive.Bindings;
using System.ComponentModel;
using TODOManager.Domain.DomainModel;
using TODOManager.Presentation.ViewModels.interfaces;
using Unity;
using System.Diagnostics;
using TODOManager.Presentation.Models;
using System.Collections.Generic;
using System;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;
using Prism.Navigation;
using System.Linq;

namespace TODOManager.Presentation.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDestructible
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("TODO Manager");

        public ReadOnlyReactiveCollection<TodoItem> TodoItems { get; }
        public ReactiveCommand AddCommend { get; }

        public IMainWindowModel mainWindowModel { get; set; }

        private CompositeDisposable disposables = new CompositeDisposable();


        public MainWindowViewModel(IMainWindowModel mainWindowModel)
        {
            this.mainWindowModel = mainWindowModel;
            //todoアイテムを読みだす
            this.TodoItems = this.mainWindowModel.todoItems
                .ToReadOnlyReactiveCollection()
                .AddTo(this.disposables);

            this.AddCommend = new ReactiveCommand();
            this.AddCommend.Subscribe(() => this.AddTodoItem());
        }

        /// <summary>
        /// 本当は引数を取るようにしないと行けない
        /// </summary>
        public void AddTodoItem()
        {
            this.mainWindowModel.AddTodoItem();
        }


        public void Destroy()
        {
            this.disposables.Dispose();
        }
    }
}
