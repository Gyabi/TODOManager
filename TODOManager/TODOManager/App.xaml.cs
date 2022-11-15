using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Windows;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Infrastructure.Factory;
using TODOManager.Presentation.Models;
using TODOManager.Presentation.ViewModels;
using TODOManager.Presentation.ViewModels.interfaces;
using TODOManager.Presentation.Views;

namespace TODOManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Model
            containerRegistry.RegisterSingleton<IMainWindowModel, MainWindowModel>();

            //Factory
            containerRegistry.RegisterSingleton<IProjectFactory, ProjectFactory>();
            containerRegistry.RegisterSingleton<ITodoItemFactory, TodoItemFactory>();

            //ポップアップの登録
            containerRegistry.RegisterDialog<AddTodoDialog, AddTodoDialogViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //V - VMの紐づけ
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}
