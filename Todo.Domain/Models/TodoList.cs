using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Todo.Domain.Models
{
    /// <summary>
    /// Represents a to-do list.
    /// </summary>
    public class TodoList : ICloneable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TodoList"/> class with the same property values as the current instance.
        /// </summary>
        /// <returns>A new <see cref="TodoList"/> object that is a copy of the current instance.</returns>
        public object Clone()
        {
            TodoList list = new TodoList
            {
                TodoItems = this.TodoItems?.Select(item => (TodoItem)item.Clone()).ToList(),
                Title = this.Title,
                IsHidden = this.IsHidden,
                CreationDate = this.CreationDate,
            };

            return list;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the to-do list.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the to-do list.
        /// </summary>
        public virtual List<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// Gets or sets the title of the to-do list.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the to-do list is hidden or not.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets the creation date for the to-do list.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
