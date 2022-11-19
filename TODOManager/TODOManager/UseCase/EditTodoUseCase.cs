using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Helpers;
using TODOManager.Presentation.ViewModels.Contents;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class EditTodoUseCase : IEditTodoUseCase
    {
        public EditTodoUseCase()
        {
        }

        public void Execute(TodoItemID targetID, ObservableCollection<TodoItem> todoItems, ObservableCollection<Project> projects, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            int index = TodoItemHelper.FindTodoIndexByID(todoItems.ToList(), targetID);
            Enum.TryParse<Priority>(priority, out Priority priorityEnum);
            Project newProject = ProjectHelper.GetProjByStr(projects, project);
            ProjectID newProjectID = (newProject == null) ? projects[0].projectID : newProject.projectID;

            TodoItem editedItem = new TodoItem(
                    itemName,
                    targetID,
                    todoItems[index].projectID,
                    useDeadLine,
                    deadLine,
                    priorityEnum,
                    new Detail(detail)
                );

            todoItems[index] = editedItem;
        }
    }
}