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

        public IProjectFactory projectFactory;
        public ITodoItemFactory todoItemFactory;

        public MainWindowModel(IProjectFactory projectFactory, ITodoItemFactory todoItemFactory)
        {
            List<TodoItem> sample = new List<TodoItem>() { new TodoItem("child", new TodoItemID("itemid"), new ProjectID("projectid"), true, DateTime.Now, Priority.NONE, new Detail("detail"), null, new List<TodoItem>()) };
            Detail detailSample = new Detail("testtesttesttesttesttesttesttesttesttesttest\ntestestsetestsetest");
            
            todoItems = new ObservableCollection<TodoItem>();
            todoItems.Add(new TodoItem("test1", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.HIGH, detailSample, null, sample));
            todoItems.Add(new TodoItem("test2", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));
            todoItems.Add(new TodoItem("test3", new TodoItemID("itemid"), new ProjectID("projectid"),true, DateTime.Now, Priority.MEDIUM, new Detail("detail"), null, new List<TodoItem>()));

            //プロジェクトを定義(本当はここでリポジトリから注入)
            this.projects = new ObservableCollection<Project>() { new Project("project1", new ProjectID("id1")), new Project("project2", new ProjectID("id2")) };
            //プライオリティを定義
            this.priorities = new ObservableCollection<Priority>();
            foreach(Priority priority in Enum.GetValues(typeof(Priority)))
            {
                this.priorities.Add(priority);
            }

            this.projectFactory = projectFactory;
            this.todoItemFactory = todoItemFactory;
        }

        public void AddTodoItem(string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            Enum.TryParse<Priority>(priority, out Priority priorityEnum);
            Project newProject = GetProjectIDFromString(project);
            ProjectID newProjectID = (newProject == null) ? this.projects[0].projectID : newProject.projectID;

            TodoItem addItem = new TodoItem(itemName, this.todoItemFactory.CreateTodoItemID(), newProjectID, useDeadLine, deadLine, priorityEnum, new Detail(detail), null, new List<TodoItem>());
            this.todoItems.Add(addItem);
        }

        /// <summary>
        /// テキストからProjectを検索して返却
        /// </summary>
        /// <returns></returns>
        public Project GetProjectIDFromString(string project)
        {
            Project outProject = null;
            foreach(Project _project in projects)
            {
                if(_project.projectName == project)
                {
                    outProject = _project;
                }
            }

            return outProject;
        }
    }
}
