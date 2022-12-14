using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Windows;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.Infrastructure.Factory;
using TODOManager.Infrastructure.Repository;
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

            //Repository
            containerRegistry.RegisterSingleton<ITodoRepository, TodoRepository>();
            containerRegistry.RegisterSingleton<IProjectRepository, ProjectRepository>();

            //UseCase
            containerRegistry.Register(typeof(IAddTodoUseCase), typeof(AddTodoUseCase));
            containerRegistry.Register(typeof(IDeleteTodoUseCase), typeof(DeleteTodoUseCase));
            containerRegistry.Register(typeof(IChangeChildTodoStatusUseCase), typeof(ChangeChildTodoStatusUseCase));
            containerRegistry.Register(typeof(ISortTodoUseCase), typeof(SortTodoUseCase));
            containerRegistry.Register(typeof(IEditTodoUseCase), typeof(EditTodoUseCase));
            containerRegistry.Register(typeof(IDeleteProjectUseCase), typeof(DeleteProjectUseCase));
            containerRegistry.Register(typeof(IAddProjectUseCase), typeof(AddProjectUseCase));
            containerRegistry.Register(typeof(IReadTodoUseCase), typeof(ReadTodoUseCase));
            containerRegistry.Register(typeof(IReadProjectUseCase), typeof(ReadProjectUseCase));

            //ポップアップの登録
            containerRegistry.RegisterDialog<AddTodoDialog, AddTodoDialogViewModel>();
            containerRegistry.RegisterDialog<EditTodoDialog, EditTodoDialogViewModel>();
            containerRegistry.RegisterDialog<EditProjectDialog, EditProjectDialogViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //V - VMの紐づけ
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}
