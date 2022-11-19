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
using Prism.Mvvm;
using TODOManager.Helpers;
using System.Security.RightsManagement;

namespace TODOManager.Presentation.Models
{
    /// <summary>
    /// MainWindowに対するModelクラス
    /// ApplicationServise層へ処理を委託する
    /// </summary>
    public class MainWindowModel : BindableBase, IMainWindowModel
    {
        //Todoアイテムのリスト
        public ObservableCollection<TodoItem> todoItems { get; }
        //プロジェクトを示すリスト
        public ObservableCollection<Project> projects { get; set; }
        //プライオリティを示すリスト
        public ObservableCollection<Priority> priorities { get; set; }

        public IAddTodoUseCase addTodoUseCase;
        public IChangeChildTodoStatusUseCase changeChildTodoStatusUseCase;
        public IDeleteTodoUseCase deleteTodoUseCase;
        public ISortTodoUseCase sortTodoUseCase;
        public IEditTodoUseCase editTodoUseCase;
        public IDeleteProjectUseCase deleteProjectUseCase;
        public IAddProjectUseCase addProjectUseCase;

        public MainWindowModel(IAddTodoUseCase addTodoUseCase, IChangeChildTodoStatusUseCase changeChildTodoStatusUseCase, IDeleteTodoUseCase deleteTodoUseCase,
            ISortTodoUseCase sortTodoUseCase, IEditTodoUseCase editTodoUseCase,
            IDeleteProjectUseCase deleteProjectUseCase, IAddProjectUseCase addProjectUseCase)
        {
            //インジェクション
            this.addTodoUseCase = addTodoUseCase;
            this.changeChildTodoStatusUseCase = changeChildTodoStatusUseCase;
            this.deleteTodoUseCase = deleteTodoUseCase;
            this.sortTodoUseCase = sortTodoUseCase;
            this.editTodoUseCase = editTodoUseCase;
            this.deleteProjectUseCase = deleteProjectUseCase;
            this.addProjectUseCase = addProjectUseCase;

            List<TodoItem> sample = new List<TodoItem>() { new TodoItem("child", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.NONE, new Detail("detail")) };
            Detail detailSample = new Detail("testtesttesttesttesttesttesttesttesttesttest\ntestestsetestsetest");
            
            todoItems = new ObservableCollection<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.HIGH, detailSample));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.MEDIUM, new Detail("detail\ntest\n>aaaaaaaaa")));
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

        /// <summary>
        /// 子要素のステータスを変更する
        /// TODO：コレクションの状態変化通知の為にリストそのものを変更するようにしている
        /// </summary>
        /// <param name="id"></param>
        /// <param name="row"></param>
        public void ChangeChildItemStatus(TodoItemID id, int row)
        {
            this.changeChildTodoStatusUseCase.Execute(this.todoItems, id, row);
        }

        /// <summary>
        /// アイテムを削除する
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTodoItem(TodoItemID id)
        {
            this.deleteTodoUseCase.Execute(this.todoItems, id);
        }

        /// <summary>
        /// 並べ変えを行う
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SortTodoItem(int from, int to)
        {
            this.sortTodoUseCase.Execute(this.todoItems, from, to);
        }
        /// <summary>
        /// IDからデータを読みだす
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TodoItem GetTodoItemData(TodoItemID id)
        {
            return TodoItemHelper.GetTodoByID(this.todoItems.ToList(), id);
        }

        /// <summary>
        /// アイテムを編集する
        /// </summary>
        /// <param name="targetID"></param>
        /// <param name="itemName"></param>
        /// <param name="projectID"></param>
        /// <param name="useDeadLine"></param>
        /// <param name="deadLine"></param>
        /// <param name="priority"></param>
        /// <param name="detail"></param>
        public void EditTodoItem(TodoItemID targetID, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            this.editTodoUseCase.Execute(targetID, this.todoItems, this.projects ,itemName, project, useDeadLine, deadLine, priority, detail);
        }

        public void DeleteProject(string projectName)
        {
            this.deleteProjectUseCase.Execute(this.projects, projectName);
        }
        public void AddProject(string projectName)
        {
            this.addProjectUseCase.Execute(this.projects, projectName);
        }
    }
}
