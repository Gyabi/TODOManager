using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Factory;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.Presentation.ViewModels.Contents;
using TODOManager.UseCase.interfaces;

namespace TODOManager.UseCase
{
    public class EditTodoUseCase : IEditTodoUseCase
    {
        ITodoRepository todoRepository;
        public EditTodoUseCase(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public void Execute(TodoItemID targetID, ObservableCollection<TodoItem> todoItems, ObservableCollection<Project> projects, string itemName, string project, bool useDeadLine, DateTime deadLine, string priority, string detail)
        {
            int index = TodoItemHelper.FindTodoIndexByID(todoItems.ToList(), targetID);
            Enum.TryParse<Priority>(priority, out Priority priorityEnum);
            Project newProject = ProjectHelper.GetProjByStr(projects, project);
            ProjectID newProjectID = (newProject == null) ? new ProjectID("") : newProject.projectID;

            TodoItem editedItem = new TodoItem(
                    itemName,
                    targetID,
                    newProjectID,
                    useDeadLine,
                    deadLine,
                    priorityEnum,
                    new Detail(detail)
                );

            todoItems[index] = editedItem;

            this.todoRepository.UpdateData(editedItem.id, editedItem, index);
        }
    }
}