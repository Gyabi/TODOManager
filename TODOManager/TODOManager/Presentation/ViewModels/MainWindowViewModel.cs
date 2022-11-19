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
        public ReactiveCommand<TodoItemVM> DeleteCommand { get; }
        public ReactiveCommand<TodoItemVM> EditCommand { get; }
        public ReactiveCommand EditProjectCommand { get; }

        public IMainWindowModel mainWindowModel { get; set; }

        //ポップアップ表示用
        private IDialogService dialogService;

        private CompositeDisposable disposables = new CompositeDisposable();

        //D＆Dのビヘイビア用
        public Action<int> DropCallback { get { return OnDrop; } }
        public ReactiveProperty<int> CurrentIndex { get; set; } = new ReactiveProperty<int>(0);

        public MainWindowViewModel(IMainWindowModel mainWindowModel, IDialogService dialogService)
        {
            this.mainWindowModel = mainWindowModel;
            //todoアイテムを読みだす
            this.TodoItems = this.mainWindowModel.todoItems
                .ToReadOnlyReactiveCollection(x => new TodoItemVM(x, this.mainWindowModel.projects))
                .AddTo(this.disposables);

            this.AddCommend = new ReactiveCommand()
                .WithSubscribe(() => this.AddTodoItem())
                .AddTo(this.disposables);

            this.DoneCommand = new ReactiveCommand<TodoItemChildVM>()
                .WithSubscribe((TodoItemChildVM child) => this.DoneChildTodoItem(child))
                .AddTo(this.disposables);

            this.DeleteCommand = new ReactiveCommand<TodoItemVM>()
                .WithSubscribe((TodoItemVM item) => this.DeleteTodoItem(item))
                .AddTo(this.disposables);

            this.EditCommand = new ReactiveCommand<TodoItemVM>()
                .WithSubscribe((TodoItemVM item) => this.EditTodoItem(item))
                .AddTo(this.disposables);

            this.EditProjectCommand = new ReactiveCommand()
                .WithSubscribe(() => this.EditProjects())
                .AddTo(this.disposables);

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

        /// <summary>
        /// アイテムを削除する
        /// </summary>
        /// <param name="item"></param>
        public void DeleteTodoItem(TodoItemVM item)
        {
            this.mainWindowModel.DeleteTodoItem(item.id);
        }

        /// <summary>
        /// アイテムを編集する
        /// </summary>
        /// <param name="item"></param>
        public void EditTodoItem(TodoItemVM item)
        {
            IDialogParameters param = new DialogParameters();
            param.Add("targetID", item.id);
            this.dialogService.ShowDialog("EditTodoDialog", param, null);
        }

        /// <summary>
        /// D&Dの動作を記述する
        /// </summary>
        /// <param name="index">移動先の番号</param>
        public void OnDrop(int index)
        {
            this.mainWindowModel.SortTodoItem(this.CurrentIndex.Value, index);
        }

        /// <summary>
        /// プロジェクトの設定画面を表示する
        /// </summary>
        public void EditProjects()
        {
            this.dialogService.ShowDialog("EditProjectDialog", null, null);
        }

        public void Destroy()
        {
            this.disposables.Dispose();
        }
    }
}
