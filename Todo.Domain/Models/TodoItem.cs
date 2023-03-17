using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Todo.Domain.Enums;

namespace Todo.Domain.Models
{
    /// <summary>
    /// Represents an item of a to-do list.
    /// </summary>
    public class TodoItem : ICloneable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TodoItem"/> class with the same property values as the current instance.
        /// </summary>
        /// <returns>A new <see cref="TodoItem"/> object that is a copy of the current instance.</returns>
        public object Clone()
        {
            TodoItem item = new TodoItem
            {
                Title = this.Title,
                Description = this.Description,
                CreationDate = this.CreationDate,
                DueDate = this.DueDate,
                Status = this.Status,
                Remind = this.Remind,
                Favorite = this.Favorite,
            };

            return item;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the id of the to-do list the item belongs to.
        /// </summary>
        public int TodoListId { get; set; }

        /// <summary>
        /// Gets or sets the to-do list the item belongs to.
        /// </summary>
        public virtual TodoList TodoList { get; set; }

        /// <summary>
        /// Gets or sets the title of the item.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the creation date for the item.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the due date for the item.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the item.
        /// </summary>
        [Range(0, 2)]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item has reminder or not.
        /// </summary>
        public bool Remind { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item belongs to favorites or not.
        /// </summary>
        public bool Favorite { get; set; }
    }
}
