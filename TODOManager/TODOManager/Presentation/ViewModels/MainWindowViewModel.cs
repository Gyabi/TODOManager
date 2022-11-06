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

namespace TODOManager.Presentation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("TODO Manager");

        public ReactiveCollection<TodoItem> TodoItems { get; }
        public ReactiveCommand AddCommend { get; }

        public IMainWindowModel mainWindowModel { get; set; }


        public MainWindowViewModel(IMainWindowModel mainWindowModel)
        {
            this.mainWindowModel = mainWindowModel;
            //todoアイテムを読みだす
            this.TodoItems = this.mainWindowModel.GetTodoItems();

            this.AddCommend = new ReactiveCommand();
            this.AddCommend.Subscribe(() => this.AddTodoItem());
        }

        /// <summary>
        /// 本当は引数を取るようにしないと行けない
        /// </summary>
        public void AddTodoItem()
        {
            TodoItem addItem = new TodoItem("add", new TodoItemID("itemid"), new ProjectID("projectid"), DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItemID>());
            this.TodoItems.Add(addItem);
            this.mainWindowModel.AddTodoItem(addItem);
        }
    }
}
