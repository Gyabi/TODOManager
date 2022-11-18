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
using TODOManager.Presentation.ViewModels.Contents;

namespace TODOManager.Presentation.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDestructible
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("TODO Manager");

        public ReadOnlyReactiveCollection<TodoItemVM> TodoItems { get; }
        public ReactiveCommand AddCommend { get; }
        public ReactiveCommand<TodoItemChildVM> DoneCommand { get; }

        public IMainWindowModel mainWindowModel { get; set; }

        //ポップアップ表示用
        private IDialogService dialogService;

        private CompositeDisposable disposables = new CompositeDisposable();


        public MainWindowViewModel(IMainWindowModel mainWindowModel, IDialogService dialogService)
        {
            this.mainWindowModel = mainWindowModel;
            //todoアイテムを読みだす
            this.TodoItems = this.mainWindowModel.todoItems
                .ToReadOnlyReactiveCollection(x => new TodoItemVM(x, this.mainWindowModel.projects))
                .AddTo(this.disposables);

            this.AddCommend = new ReactiveCommand()
            .WithSubscribe(() => this.AddTodoItem());

            this.DoneCommand = new ReactiveCommand<TodoItemChildVM>()
                .WithSubscribe((TodoItemChildVM child) => this.DoneChildTodoItem(child));

            this.dialogService = dialogService;
        }

        /// <summary>
        /// Todoを生成
        /// </summary>
        public void AddTodoItem()
        {
            this.dialogService.ShowDialog("AddTodoDialog", null, null);
        }

        /// <summary>
        /// 子タスクのステータスを変更する
        /// </summary>
        public void DoneChildTodoItem(TodoItemChildVM child)
        {
            this.mainWindowModel.ChangeChildItemStatus(child.pearentItem.id, child.row);
        }

        public void Destroy()
        {
            this.disposables.Dispose();
        }
    }
}
