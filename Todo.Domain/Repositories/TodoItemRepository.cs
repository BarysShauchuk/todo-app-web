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
    /// Repository class that implements the <see cref="IRepository{TEntity}"/> interface for the <see cref="TodoItem"/> model.
    /// </summary>
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly TodoDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="TodoDbContext"/> object used to perform database operations.</param>
        public TodoItemRepository(TodoDbContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            this.context = context;
        }


        /// <summary>
        /// Gets a <see cref="TodoItem"/> object from the database with a specified id.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="TodoItem"/> object to be retrieved.</param>
        /// <returns>The <see cref="TodoItem"/> object with the specified id.</returns>
        public TodoItem GetById(int id)
        {
            return this.context.TodoItems.Find(id);
        }

        /// <summary>
        /// Gets a list of all <see cref="TodoItem"/> objects from the database.
        /// </summary>
        /// <returns>A list of all <see cref="TodoItem"/> objects in the database.</returns>
        public List<TodoItem> GetAll()
        {
            return this.context.TodoItems.ToList();
        }

        /// <summary>
        /// Adds a <see cref="TodoItem"/> object to the database.
        /// </summary>
        /// <param name="todoItem">The <see cref="TodoItem"/> object to be added to the database.</param>
        public void Add(TodoItem todoItem)
        {
            _ = todoItem ?? throw new ArgumentNullException(nameof(todoItem));

            this.context.TodoItems.Add(todoItem);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Updates a <see cref="TodoItem"/> object in the database.
        /// </summary>
        /// <param name="todoItem">The updated <see cref="TodoItem"/> object to be saved to the database.</param>
        public void Update(TodoItem todoItem)
        {
            _ = todoItem ?? throw new ArgumentNullException(nameof(todoItem));

            this.context.TodoItems.Update(todoItem);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a <see cref="TodoItem"/> object from the database.
        /// </summary>
        /// <param name="todoItem">The <see cref="TodoItem"/> object to be deleted from the database.</param>
        public void Delete(TodoItem todoItem)
        {
            _ = todoItem ?? throw new ArgumentNullException(nameof(todoItem));

            this.context.TodoItems.Remove(todoItem);
            this.context.SaveChanges();
        }
    }
}
