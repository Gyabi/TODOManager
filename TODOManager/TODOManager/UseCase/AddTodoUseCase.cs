using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Helpers;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class AddTodoUseCase : IAddTodoUseCase
    {

        private ITodoItemFactory todoItemFactory;
        public AddTodoUseCase(ITodoItemFactory todoItemFactory)
        {
            this.todoItemFactory = todoItemFactory;
        }

        public void Execute(ObservableCollection<TodoItem> todoItems, ObservableCollection<Project> projects, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            Enum.TryParse<Priority>(priority, out Priority priorityEnum);
            Project newProject = ProjectHelper.GetProjByStr(projects, project);
            ProjectID newProjectID = (newProject == null) ? projects[0].projectID : newProject.projectID;
            TodoItem addItem = new TodoItem(itemName, this.todoItemFactory.CreateTodoItemID(), newProjectID, useDeadLine, deadLine, priorityEnum, new Detail(detail));

            todoItems.Add(addItem);
        }
    }
}