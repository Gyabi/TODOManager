using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using MahApps.Metro.Converters;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using TODOManager.Domain.DomainModel;
using TODOManager.Presentation.ViewModels.interfaces;

namespace TODOManager.Presentation.ViewModels
{
    public class AddTodoDialogViewModel: BindableBase, IDialogAware, IDisposable
    {
        #region Bind
        /// <summary>メッセージボックスのタイトルを取得します。</summary>
        public string Title => "AddTodo";
        /// <summary>メッセージボックスへ表示する文字列を取得します。</summary>
        public ReactivePropertySlim<string> ItemName { get; }

        /// <summary>デッドラインを記入するかチェックボックス</summary>
        public ReactivePropertySlim<bool> UseDeadLine { get; }
        /// <summary>デッドラインを記入するか選択</summary>
        public ReactivePropertySlim<Visibility> ShowDeadLine { get; }

        /// <summary>期限</summary>
        public ReactivePropertySlim<DateTime> DeadLine { get; }

        /// <summary>詳細</summary>
        public ReactivePropertySlim<string> Detail { get; }

        /// <summary>プロジェクトの選択肢</summary>
        public ReadOnlyReactiveCollection<string> ProjectElements { get; }

        /// <summary>選択したプロジェクト</summary>
        public ReactivePropertySlim<string> Project { get; }

        /// <summary>プライオリティの選択肢</summary>
        public ReadOnlyReactiveCollection<string> PriorityElements { get; }
        /// <summary>選択したプライオリティ</summary>
        public ReactivePropertySlim<string> Priority { get; }

        /// <summary>SubmitボタンのCommand</summary>
        public ReactiveCommand SubmitCommand { get; } = new ReactiveCommand();

        /// <summary>CancelボタンのCommand</summary>
        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();
        #endregion

        #region Member
        /// <summary>modelオブジェクト</summary>
        public IMainWindowModel mainWindowModel;

        /// <summary>ダイアログのCloseを要求するAction。</summary>
        public event Action<IDialogResult> RequestClose;

        /// <summary>dispose用</summary>
        private CompositeDisposable disposables = new CompositeDisposable();
        #endregion

        /// <summary>ダイアログがClose可能かを取得します。</summary>
        /// <returns></returns>
        public bool CanCloseDialog() { return true; }

        /// <summary>ダイアログClose時のイベントハンドラ。</summary>
        public void OnDialogClosed(){ }

        /// <summary>ダイアログOpen時のイベントハンドラ。</summary>
        /// <param name="parameters">IDialogServiceに設定されたパラメータを表すIDialogParameters。</param>
        public void OnDialogOpened(IDialogParameters parameters){}

        public AddTodoDialogViewModel(IMainWindowModel mainWindowModel)
        {
            //DIでモデルを取得
            this.mainWindowModel = mainWindowModel;

            //各種バインド用のメンバを生成
            this.ItemName = new ReactivePropertySlim<string>(string.Empty);
            this.UseDeadLine = new ReactivePropertySlim<bool>(false);
            this.ShowDeadLine = new ReactivePropertySlim<Visibility>(Visibility.Hidden);
            this.UseDeadLine.Subscribe(ChangeShowDeadLine).AddTo(this.disposables);
            this.DeadLine = new ReactivePropertySlim<DateTime>(DateTime.Today);
            this.Detail = new ReactivePropertySlim<string>("detail");
            this.Project = new ReactivePropertySlim<string>(string.Empty);
            this.Priority = new ReactivePropertySlim<string>(string.Empty);

            //選択肢を示す配列をモデルから取得
            this.ProjectElements = this.mainWindowModel.projects
                .ToReadOnlyReactiveCollection(x => x.projectName)
                .AddTo(this.disposables);
            this.PriorityElements = this.mainWindowModel.priorities
                .ToReadOnlyReactiveCollection(x => x.ToString())
                .AddTo(this.disposables);

            //コマンドの設定
            this.SubmitCommand.Subscribe(SubmitCallBack).AddTo(this.disposables);
            this.CancelCommand.Subscribe(CancelCallBack).AddTo(this.disposables);
        }

        /// <summary>
        /// Submit押下時の処理
        /// </summary>
        public void SubmitCallBack()
        {
            //入力内容からデータを取得してModel経由で保存
            this.mainWindowModel.AddTodoItem(this.ItemName.Value, this.Project.Value, this.UseDeadLine.Value, this.DeadLine.Value, this.Priority.Value, this.Detail.Value);
            //画面終了命令
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }
        /// <summary>
        /// Cancel押下時の処理
        /// </summary>
        public void CancelCallBack()
        {
            //画面終了命令
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        /// <summary>
        /// DeadLineの表示を切り替える
        /// </summary>
        /// <param name="useDeadLine"></param>
        public void ChangeShowDeadLine(bool useDeadLine)
        {
            if(useDeadLine)
            {
                this.ShowDeadLine.Value = Visibility.Visible;
            }
            else
            {
                this.ShowDeadLine.Value = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 破棄のタイミングで呼ばれるメソッド
        /// </summary>
        public void Dispose()
        {
            this.disposables.Dispose();
        }

    }
}
