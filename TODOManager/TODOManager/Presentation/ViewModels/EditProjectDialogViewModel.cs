using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class EditProjectDialogViewModel: BindableBase, IDialogAware, IDisposable
    {
        /// <summary>メッセージボックスのタイトルを取得します。</summary>
        public string Title => "EditProject";

        public ReadOnlyReactiveCollection<string> Projects { get; }
        public ReactiveCommand<string> DeleteCommand { get; }
        public ReactiveCommand AddCommand { get; }
        public ReactiveProperty<string> newProjectName { get; set; } = new ReactiveProperty<string>(string.Empty);

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

        public EditProjectDialogViewModel(IMainWindowModel mainWindowModel)
        {
            //DIでモデルを取得
            this.mainWindowModel = mainWindowModel;

            this.Projects = this.mainWindowModel.projects
                .ToReadOnlyReactiveCollection(x => x.projectName)
                .AddTo(this.disposables);

            this.DeleteCommand = new ReactiveCommand<string>()
                .WithSubscribe((string projectName) => this.DeleteProject(projectName))
                .AddTo(this.disposables);

            this.AddCommand = new ReactiveCommand()
                .WithSubscribe(() => this.AddProject())
                .AddTo(this.disposables);
        }

        /// <summary>
        /// Submit押下時の処理
        /// </summary>
        public void SubmitCallBack()
        {
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

        public void DeleteProject(string projectName)
        {
            this.mainWindowModel.DeleteProject(projectName);
        }

        public void AddProject()
        {
            this.mainWindowModel.AddProject(this.newProjectName.Value);
            this.newProjectName.Value = string.Empty;
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
