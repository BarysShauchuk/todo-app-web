using NUnit.Framework;
using System.Collections.Generic;
using System;
using Todo.Domain.Data;
using Todo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Domain.Enums;
using System.Linq;

namespace Todo.Tests
{
    [TestFixture]
    public sealed class RepositoryTests
    {
        private TodoItemRepository todoItemRepository;
        private TodoListRepository todoListRepository;

        [OneTimeSetUp]
        public void InitDb()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            var context = new TodoDbContext(options);

            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
                this.todoItemRepository = new TodoItemRepository(context);
                this.todoListRepository = new TodoListRepository(context);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        [Test]
        public void TodoItemConstructor_NullContext_ThrowsArgumentNullException()
        {
            TodoDbContext nullContext = null;
            Assert.Throws<ArgumentNullException>(() => new TodoItemRepository(nullContext));
        }

        [Test]
        public void Add_NullTodoItem_ThrowsArgumentNullException()
        {
            TodoItem nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoItemRepository.Add(nullTodoItem));
        }

        [Test]
        public void Update_NullTodoItem_ThrowsArgumentNullException()
        {
            TodoItem nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoItemRepository.Update(nullTodoItem));
        }

        [Test]
        public void Delete_NullTodoItem_ThrowsArgumentNullException()
        {
            TodoItem nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoItemRepository.Delete(nullTodoItem));
        }

        [Test]
        public void TodoListConstructor_NullContext_ThrowsArgumentNullException()
        {
            TodoDbContext nullContext = null;
            Assert.Throws<ArgumentNullException>(() => new TodoListRepository(nullContext));
        }

        [Test]
        public void Add_NullTodoList_ThrowsArgumentNullException()
        {
            TodoList nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoListRepository.Add(nullTodoItem));
        }

        [Test]
        public void Update_NullTodoList_ThrowsArgumentNullException()
        {
            TodoList nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoListRepository.Update(nullTodoItem));
        }

        [Test]
        public void Delete_NullTodoList_ThrowsArgumentNullException()
        {
            TodoList nullTodoItem = null;
            Assert.Throws<ArgumentNullException>(() => this.todoListRepository.Delete(nullTodoItem));
        }

        [Test]
        [Order(0)]
        public void Add_ValidTodoListWithTodoItems_WereAdded()
        {
            int initialTodoItemsNumber = this.todoItemRepository.GetAll().Count;
            int initialTodoListsNumber = this.todoListRepository.GetAll().Count;

            var toDoList = new TodoList
            {
                Title = "Home Tasks",
                TodoItems = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Status = Status.InProgress,
                        Title = "Clean the kitchen",
                        Description = "Wipe down the counters and mop the floor",
                        DueDate = new DateTime(2023, 2, 15),
                    },
                    new TodoItem
                    {
                        Status = Status.InProgress,
                        Title = "Read a book",
                        Description = "Finish reading 'To Kill a Mockingbird'",
                        DueDate = new DateTime(2023, 2, 20),
                    },
                },
            };

            this.todoListRepository.Add(toDoList);

            Assert.AreEqual(2 + initialTodoItemsNumber, this.todoItemRepository.GetAll().Count);
            Assert.AreEqual(1 + initialTodoListsNumber, this.todoListRepository.GetAll().Count);
        }

        [Test]
        [Order(1)]
        public void Add_ValidTodoItem_Update_Title_Test()
        {
            int initialTodoItemsNumber = this.todoItemRepository.GetAll().Count;

            var entry = new TodoItem
            {
                Status = Status.InProgress,
                Title = "Go grocery shopping",
                Description = "Buy milk, bread, and eggs",
                DueDate = new DateTime(2023, 2, 10),
            };

            var toDoList = this.todoListRepository.GetAll().First(x => x.Title == "Home Tasks");
            toDoList.TodoItems.Add(entry);
            toDoList.Title = "Boring Tasks";
            this.todoListRepository.Update(toDoList);

            Assert.AreEqual(
                "Boring Tasks",
                this.todoListRepository.GetById(toDoList.Id).Title);
            Assert.AreEqual(
                initialTodoItemsNumber + 1,
                this.todoListRepository.GetById(toDoList.Id).TodoItems.Count);
        }

        [Test]
        [Order(2)]
        public void Delete_ValidTodoItem_Test()
        {
            int initialTodoItemsNumber = this.todoItemRepository.GetAll().Count;

            var toDoList = this.todoListRepository.GetAll().First(x => x.Title == "Boring Tasks");
            var entry = toDoList.TodoItems.First();
            this.todoItemRepository.Delete(entry);

            Assert.AreEqual(
                initialTodoItemsNumber - 1,
                this.todoListRepository.GetById(toDoList.Id).TodoItems.Count);
        }

        [Test]
        [Order(3)]
        public void Delete_ValidList_Test()
        {
            int initialTodoListsNumber = this.todoListRepository.GetAll().Count;
            int initialTodoItemsNumber = this.todoItemRepository.GetAll().Count;

            var toDoList = this.todoListRepository.GetAll().First(x => x.Title == "Boring Tasks");

            int listTodoItemsNumber = toDoList.TodoItems.Count;

            this.todoListRepository.Delete(toDoList);

            Assert.AreEqual(
                initialTodoListsNumber - 1,
                this.todoListRepository.GetAll().Count);
            Assert.AreEqual(
                initialTodoItemsNumber - listTodoItemsNumber,
                this.todoListRepository.GetAll().Count);
        }
    }
}
