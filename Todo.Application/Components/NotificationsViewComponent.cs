using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Application.Components
{
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IRepository<TodoItem> itemRepository;

        public NotificationsViewComponent(IRepository<TodoItem> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IViewComponentResult Invoke()
        {
            var items = itemRepository
                .GetAll()
                .Where(item => item.Remind && item.DueDate.Value.Date <= DateTime.Today)
                .OrderBy(item => item.DueDate)
                .Select(item => (item, (DateTime.Today - item.DueDate.Value.Date).TotalDays));

            return View(items);
        }
    }
}
