using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Todo.Application.Models.ViewModels;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Application.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IRepository<TodoList> listRepository;

        public NavigationMenuViewComponent(IRepository<TodoList> listRepository)
        {
            this.listRepository = listRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedListId = 0;
            ViewBag.SelectedSection = string.Empty;
            ViewBag.SelectedCurrentList = false;
            ViewBag.SelectedArchivedList = false;

            string controller = (RouteData?.Values["controller"] ?? string.Empty).ToString();
            string action = (RouteData?.Values["action"] ?? string.Empty).ToString();
            string id = (RouteData?.Values["id"] ?? string.Empty).ToString();

            if (controller == "Home")
            {
                ViewBag.SelectedSection = action;
            }

            if (controller == "TodoList" && id != string.Empty)
            {
                int idResult = 0;
                if (int.TryParse(id, out idResult))
                {
                    var list = this.listRepository.GetById(idResult);

                    if (list != null)
                    {
                        ViewBag.SelectedListId = list.Id;
                        ViewBag.SelectedCurrentList = !list.IsHidden;
                        ViewBag.SelectedArchivedList = list.IsHidden;
                    }
                }
            }

            var lists = listRepository.GetAll().OrderByDescending(x => x.CreationDate);
            return View(new NavigationMenuViewModel
            {
                CurrentLists = lists.Where(list => !list.IsHidden)
                                    .Select(x => new TodoListViewModel(x)),

                HiddenLists = lists.Where(list => list.IsHidden)
                                    .Select(x => new TodoListViewModel(x))
            });
        }
    }
}
