using MahApps.Metro.Converters;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Presentation.ViewModels.interfaces;
using Reactive.Bindings.Extensions;
using TODOManager.UseCase;
using Unity;
using TODOManager.UseCase.interfaces;

namespace TODOManager.Presentation.Models
{
    /// <summary>
    /// MainWindowに対するModelクラス
    /// ApplicationServise層へ処理を委託する
    /// </summary>
    public class MainWindowModel : IMainWindowModel
    {
        //Todoアイテムのリスト
        public ObservableCollection<TodoItem> todoItems { get; }
        //プロジェクトを示すリスト
        public ObservableCollection<Project> projects { get; set; }
        //プライオリティを示すリスト
        public ObservableCollection<Priority> priorities { get; set; }

        public IAddTodoUseCase addTodoUseCase;

        public MainWindowModel(IAddTodoUseCase addTodoUseCase)
        {
            //インジェクション
            this.addTodoUseCase = addTodoUseCase;

            List<TodoItem> sample = new List<TodoItem>() { new TodoItem("child", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.NONE, new Detail("detail")) };
            Detail detailSample = new Detail("testtesttesttesttesttesttesttesttesttesttest\ntestestsetestsetest");
            
            todoItems = new ObservableCollection<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.HIGH, detailSample));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.MEDIUM, new Detail("detail")));
            todoItems.Add(new TodoItem("test3", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.MEDIUM, new Detail("detail")));

            //プロジェクトを定義(本当はここでリポジトリから注入)
            this.projects = new ObservableCollection<Project>() { new Project("project1", new ProjectID("id1")), new Project("project2", new ProjectID("id2")) };
            //プライオリティを定義
            this.priorities = new ObservableCollection<Priority>();
            foreach(Priority priority in Enum.GetValues(typeof(Priority)))
            {
                this.priorities.Add(priority);
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="project"></param>
        /// <param name="useDeadLine"></param>
        /// <param name="deadLine"></param>
        /// <param name="priority"></param>
        /// <param name="detail"></param>
        public void AddTodoItem(string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            this.addTodoUseCase.Execute(this.todoItems, this.projects, itemName, project, useDeadLine, deadLine, priority, detail);
        }
    }
}
