using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using Todo.Application.Models.ViewModels;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Application.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IRepository<TodoItem> itemRepository;

        public TodoItemController(IRepository<TodoItem> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult Create(int listId, string returnUrl)
        {
            TodoItem item = new TodoItem
            {
                TodoListId = listId,
                Status = Domain.Enums.Status.NotStarted,
                DueDate = DateTime.Now
            };

            return View(new ReturnUrlViewModel<TodoItem>(item, returnUrl));
        }

        [HttpPost]
        public IActionResult Create(ReturnUrlViewModel<TodoItem> listModel)
        {
            listModel.Model.CreationDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(listModel);
            }

            this.itemRepository.Add(listModel.Model);
            return Redirect(listModel.ReturnUrl);
        }

        public IActionResult ChangeStatus(int id, Status status, string returnUrl)
        {
            var item = this.itemRepository.GetById(id);
            item.Status = status;
            return this.Update(new ReturnUrlViewModel<TodoItem>(item, returnUrl));
        }

        public IActionResult ChangeFavorite(int id, bool favorite, string returnUrl)
        {
            var item = this.itemRepository.GetById(id);
            item.Favorite = favorite;
            return this.Update(new ReturnUrlViewModel<TodoItem>(item, returnUrl));
        }

        public IActionResult ChangeRemind(int id, bool remind, string returnUrl)
        {
            var item = this.itemRepository.GetById(id);
            item.Remind = remind;
            return this.Update(new ReturnUrlViewModel<TodoItem>(item, returnUrl));
        }

        [HttpGet]
        public IActionResult Update(int id, string returnUrl)
        {
            var item = this.itemRepository.GetById(id);
            return View(new ReturnUrlViewModel<TodoItem>(item, returnUrl));
        }

        public IActionResult Update(ReturnUrlViewModel<TodoItem> itemModel)
        {
            if (!ModelState.IsValid)
            {
                return View(itemModel);
            }

            itemModel.Model.CreationDate = DateTime.Now;
            this.itemRepository.Update(itemModel.Model);

            if (itemModel.ReturnUrl.IsNullOrEmpty())
            {
                return RedirectToAction("Index", "TodoList", new { id = itemModel.Model.TodoListId });
            }

            return Redirect(itemModel.ReturnUrl);
        }

        public IActionResult Delete(int id)
        {
            var item = this.itemRepository.GetById(id);
            int listId = item.TodoListId;
            this.itemRepository.Delete(item);

            return RedirectToAction(nameof(Index), "TodoList", new { id = listId });
        }
    }
}
