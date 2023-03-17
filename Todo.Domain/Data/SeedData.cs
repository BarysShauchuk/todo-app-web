using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.Enums;
using Todo.Domain.Models;

namespace Todo.Domain.Data
{
    /// <summary>
    /// Static class with methods for filling database with seed data.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Static class with methods for applying pending migrations to database and filling it with seed data.
        /// </summary>
        /// <param name="context">The <see cref="TodoDbContext"/> object used to perform database operations.</param>
        public static void EnsurePopulated(TodoDbContext context)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.TodoLists.Any())
            {
                context.TodoLists.AddRange(GetSeedLists());
                context.SaveChanges();
            }
        }

        private static List<TodoList> GetSeedLists()
        {
            var todoList1 = new TodoList
            {
                Title = "Personal",
                IsHidden = false,
                CreationDate = DateTime.Now.AddDays(-1),
                TodoItems = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Title = "Go for a run",
                        Description = "Run for 30 minutes around the park",
                        CreationDate = DateTime.Now.AddDays(-1),
                        DueDate = DateTime.Now.AddDays(2),
                        Status = Status.InProgress,
                        Remind = true,
                        Favorite = false,
                    },
                    new TodoItem
                    {
                        Title = "Grocery shopping",
                        Description = "Buy milk, eggs, and bread",
                        CreationDate = DateTime.Now.AddDays(-2),
                        DueDate = DateTime.Now.AddDays(1),
                        Status = Status.NotStarted,
                        Remind = true,
                        Favorite = true,
                    },
                    new TodoItem
                    {
                        Title = "Finish book",
                        Description = "Read the last chapter of the book",
                        CreationDate = DateTime.Now.AddDays(-5),
                        DueDate = DateTime.Now.AddDays(-1),
                        Status = Status.Completed,
                        Remind = false,
                        Favorite = false,
                    }
                }
            };
            
            var todoList2 = new TodoList
            {
                Title = "Work",
                IsHidden = false,
                CreationDate = DateTime.Now.AddDays(-2),
                TodoItems = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Title = "Finish report",
                        Description = "Complete the quarterly report",
                        CreationDate = DateTime.Now.AddDays(-7),
                        DueDate = DateTime.Now.AddDays(7),
                        Status = Status.NotStarted,
                        Remind = true,
                        Favorite = false,
                    },
                    new TodoItem
                    {
                        Title = "Attend meeting",
                        Description = "Attend the weekly team meeting",
                        CreationDate = DateTime.Now.AddDays(-1),
                        DueDate = DateTime.Now.AddDays(1),
                        Status = Status.InProgress,
                        Remind = true,
                        Favorite = true,
                    }
                }
            };

            var todoList3 = new TodoList
            {
                Title = "Vacation",
                IsHidden = true,
                CreationDate = DateTime.Now.AddDays(-2),
                TodoItems = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Title = "Book flight",
                        Description = "Book a flight to Hawaii",
                        CreationDate = DateTime.Now.AddDays(-14),
                        DueDate = DateTime.Now.AddDays(30),
                        Status = Status.NotStarted,
                        Remind = true,
                        Favorite = false,
                    },
                    new TodoItem
                    {
                        Title = "Pack bags",
                        Description = "Pack bags for the trip",
                        CreationDate = DateTime.Now.AddDays(-7),
                        DueDate = DateTime.Now.AddDays(7),
                        Status = Status.InProgress,
                        Remind = false,
                        Favorite = false,
                    },
                    new TodoItem
                    {
                        Title = "Relax",
                        Description = "Enjoy the vacation",
                        CreationDate = DateTime.Now.AddDays(7),
                        DueDate = DateTime.Now.AddDays(14),
                        Status = Status.NotStarted,
                        Remind = true,
                        Favorite = true,
                    }
                }
            };
            
            return new List<TodoList>
            {
                todoList1,
                todoList2,
                todoList3
            };
        }
    }
}
