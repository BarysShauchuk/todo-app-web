using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Todo.Domain.Enums;
using Todo.Domain.Models;

namespace Todo.Application.Models.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel(TodoList todoList)
        {
            this.TodoList = todoList;
        }

        public TodoList TodoList { get; set; }

        public int InProgressItemsNumber => this.TodoList.TodoItems?.Count(item => item.Status == Status.InProgress) ?? 0;

        public int CompletedItemsNumber => this.TodoList.TodoItems?.Count(item => item.Status == Status.Completed) ?? 0;

        public int NotStartedItemsNumber => this.TodoList.TodoItems?.Count(item => item.Status == Status.NotStarted) ?? 0;

        public int TotalItemsNumber => this.TodoList.TodoItems?.Count() ?? 0;

        public double InProgressItemsPercent => this.InProgressItemsNumber * 100d / this.TotalItemsNumber;

        public double CompletedItemsPercent => this.CompletedItemsNumber * 100d / this.TotalItemsNumber;
    }
}
