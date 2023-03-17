using System.Collections.Generic;

namespace Todo.Application.Models.ViewModels
{
    public class NavigationMenuViewModel
    {
        public IEnumerable<TodoListViewModel> CurrentLists { get; set; }
        public IEnumerable<TodoListViewModel> HiddenLists { get; set; }
    }
}
