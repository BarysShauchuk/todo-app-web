using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Todo.Domain.Enums
{
    /// <summary>
    /// An enumeration that represents the status of a task.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The task has not been started.
        /// </summary>
        [Display(Name = "Not started")]
        NotStarted,

        /// <summary>
        /// The task is in progress.
        /// </summary>
        [Display(Name = "In progress")]
        InProgress,

        /// <summary>
        /// The task has been completed.
        /// </summary>
        [Display(Name = "Completed")]
        Completed,
    }
}
