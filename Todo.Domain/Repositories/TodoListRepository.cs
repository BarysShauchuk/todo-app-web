using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Domain.Data;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;

namespace Todo.Domain.Repositories
{
    /// <summary>
    /// Repository class that implements the <see cref="IRepository{TEntity}"/> interface for the <see cref="TodoList"/> model.
    /// </summary>
    public class TodoListRepository : IRepository<TodoList>
    {
        private readonly TodoDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="TodoDbContext"/> object used to perform database operations.</param>
        public TodoListRepository(TodoDbContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            this.context = context;
        }

        /// <summary>
        /// Gets a <see cref="TodoList"/> object from the database with a specified id.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="TodoList"/> object to be retrieved.</param>
        /// <returns>The <see cref="TodoList"/> object with the specified id.</returns>
        public TodoList GetById(int id)
        {
            return this.context.TodoLists.Find(id);
        }

        /// <summary>
        /// Gets a list of all <see cref="TodoList"/> objects from the database.
        /// </summary>
        /// <returns>A list of all <see cref="TodoList"/> objects in the database.</returns>
        public List<TodoList> GetAll()
        {
            return this.context.TodoLists.ToList();
        }

        /// <summary>
        /// Adds a <see cref="TodoList"/> object to the database.
        /// </summary>
        /// <param name="todoList">The <see cref="TodoList"/> object to be added to the database.</param>
        public void Add(TodoList todoList)
        {
            _ = todoList ?? throw new ArgumentNullException(nameof(todoList));

            this.context.TodoLists.Add(todoList);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Updates a <see cref="TodoList"/> object in the database.
        /// </summary>
        /// <param name="todoList">The updated <see cref="TodoList"/> object to be saved to the database.</param>
        public void Update(TodoList todoList)
        {
            _ = todoList ?? throw new ArgumentNullException(nameof(todoList));

            this.context.TodoLists.Update(todoList);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a <see cref="TodoList"/> object from the database.
        /// </summary>
        /// <param name="todoList">The <see cref="TodoList"/> object to be deleted from the database.</param>
        public void Delete(TodoList todoList)
        {
            _ = todoList ?? throw new ArgumentNullException(nameof(todoList));

            this.context.TodoLists.Remove(todoList);
            this.context.SaveChanges();
        }
    }
}
