using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Windows;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Helpers;
using TODOManager.Infrastructure.Factory;
using TODOManager.Presentation.Models;
using TODOManager.Presentation.ViewModels;
using TODOManager.Presentation.ViewModels.interfaces;
using TODOManager.Presentation.Views;
using TODOManager.UseCase;
using TODOManager.UseCase.interfaces;

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

            //UseCase
            containerRegistry.Register(typeof(IAddTodoUseCase), typeof(AddTodoUseCase));
            containerRegistry.Register(typeof(IChangeChildTodoStatusUseCase), typeof(ChangeChildTodoStatusUseCase));

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
