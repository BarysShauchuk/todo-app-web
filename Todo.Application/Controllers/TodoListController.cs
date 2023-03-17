using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Application.Models.ViewModels;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Application.Controllers
{
    public class TodoListController : Controller
    {
        private readonly IRepository<TodoList> listRepository;

        public TodoListController(IRepository<TodoList> listRepository)
        {
            this.listRepository = listRepository;
        }

        public IActionResult Index(int id, bool hideCompleted)
        {
            var list = listRepository.GetById(id);
            var items = list.TodoItems ?? new List<TodoItem>();

            if (hideCompleted)
            {
                return View((
                    items
                    .Where(item => item.Status != Status.Completed)
                    .OrderByDescending(x => x.CreationDate)
                    .ToList(),
                    list,
                    hideCompleted));
            }

            return View((items.OrderByDescending(x => x.CreationDate).ToList(), list, hideCompleted));
        }

        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            var list = new TodoList();
            return View(new ReturnUrlViewModel<TodoList>(list, returnUrl));
        }

        [HttpPost]
        public IActionResult Create(ReturnUrlViewModel<TodoList> listModel)
        {
            if (!ModelState.IsValid)
            {
                return View(listModel);
            }

            listModel.Model.CreationDate = DateTime.Now;
            this.listRepository.Add(listModel.Model);
            return RedirectToAction(nameof(Index), new { id = listModel.Model.Id });
        }

        public IActionResult Delete(int id)
        {
            this.listRepository.Delete(this.listRepository.GetById(id));

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Archive(int id, string returnUrl)
        {
            var list = this.listRepository.GetById(id);
            list.IsHidden = !list.IsHidden;
            return this.Update(new ReturnUrlViewModel<TodoList>(list, returnUrl));
        }

        [HttpGet]
        public IActionResult Update(int id, string returnUrl)
        {
            var list = this.listRepository.GetById(id);
            return View(new ReturnUrlViewModel<TodoList>(list, returnUrl));
        }

        public IActionResult Update(ReturnUrlViewModel<TodoList> listModel)
        {
            if (!ModelState.IsValid)
            {
                return View(listModel);
            }

            listModel.Model.CreationDate = DateTime.Now;
            this.listRepository.Update(listModel.Model);
            return RedirectToAction(nameof(Index), new { id = listModel.Model.Id });
        }

        public IActionResult Copy(int id)
        {
            var list = this.listRepository.GetById(id);
            var newList = (TodoList)list.Clone();
            newList.Title = new string((newList.Title + " (Copy)").Take(50).ToArray());
            this.listRepository.Add(newList);

            return RedirectToAction(nameof(Index), new { id = newList.Id });
        }
    }
}
