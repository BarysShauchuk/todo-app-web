using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Domain.Data
{
    /// <summary>
    /// DbContext class for a to-do app.
    /// </summary>
    public class TodoDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoDbContext"/> class.
        /// Ensures that the database for the context exists.
        /// </summary>
        public TodoDbContext()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoDbContext"/> class with given options.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options) { }

        /// <summary>
        /// Gets or sets the Entries property as a DbSet of <see cref="TodoItem"/> objects.
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// Gets or sets the ToDoLists property as a DbSet of <see cref="TodoList"/> objects.
        /// </summary>
        public DbSet<TodoList> TodoLists { get; set; }
    }
}
