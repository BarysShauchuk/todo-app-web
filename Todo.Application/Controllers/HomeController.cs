using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.Models;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<TodoItem> itemRepository;

        public HomeController(IRepository<TodoItem> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpcomingToday(bool hideCompleted)
        {
            var items = this.itemRepository
                .GetAll()
                .Where(item => item.DueDate.Value.Date == DateTime.Today
                        && (item.Status != Status.Completed || !hideCompleted))
                .OrderBy(item => item.DueDate)
                .ToList();

            return View((items, hideCompleted));
        }

        public IActionResult WithNotifiers()
        {
            var items = this.itemRepository
                .GetAll()
                .Where(item => item.Remind)
                .OrderBy(item => item.DueDate)
                .ToList();

            return View(items);
        }

        public IActionResult Favorite(bool hideCompleted)
        {
            var items = this.itemRepository
                .GetAll()
                .Where(item => item.Favorite
                        && (item.Status != Status.Completed || !hideCompleted))
                .ToList();

            return View((items, hideCompleted));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
