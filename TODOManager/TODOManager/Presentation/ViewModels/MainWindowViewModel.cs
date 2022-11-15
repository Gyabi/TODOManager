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
using Prism.Services.Dialogs;

namespace TODOManager.Presentation.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDestructible
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("TODO Manager");

        public ReadOnlyReactiveCollection<TodoItem> TodoItems { get; }
        public ReactiveCommand AddCommend { get; }

        public IMainWindowModel mainWindowModel { get; set; }

        //ポップアップ表示用
        private IDialogService dialogService;

        private CompositeDisposable disposables = new CompositeDisposable();


        public MainWindowViewModel(IMainWindowModel mainWindowModel, IDialogService dialogService)
        {
            this.mainWindowModel = mainWindowModel;
            //todoアイテムを読みだす
            this.TodoItems = this.mainWindowModel.todoItems
                .ToReadOnlyReactiveCollection()
                .AddTo(this.disposables);

            this.AddCommend = new ReactiveCommand()
            .WithSubscribe(() => this.AddTodoItem());

            this.dialogService = dialogService;
        }

        /// <summary>
        /// 本当は引数を取るようにしないと行けない
        /// </summary>
        public void AddTodoItem()
        {
            this.dialogService.ShowDialog("AddTodoDialog", null, null);
        }


        public void Destroy()
        {
            this.disposables.Dispose();
        }
    }
}
