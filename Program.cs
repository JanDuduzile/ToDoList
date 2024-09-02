using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ToDoList
{
    class Program
    {
        enum UserChoice
        {
            AddTask = 1,
            DeleteTask,
            UpdateTask,
            SaveToFile,
            ListAllTasks,
            ListDoneTasks,
            ListNotDoneTasks,
            ListInProgressTasks,
            SearchTasks,
            Exit
        }

        enum TaskStatus
        {
            NotStarted,
            InProgress,
            Done
        }

        enum TaskPriority
        {
            Low,
            Medium,
            High
        }

        class Task
        {
            public string Description { get; set; }
            public TaskStatus Status { get; set; }
            public DateTime? DueDate { get; set; }
            public TaskPriority Priority { get; set; }

            public Task(string description, TaskStatus status = TaskStatus.NotStarted, DateTime? dueDate = null, TaskPriority priority = TaskPriority.Medium)
            {
                Description = description;
                Status = status;
                DueDate = dueDate;
                Priority = priority;
            }
        }

        static void Main(string[] args)
        {
            List<Task> toDoList = new List<Task>();

            while (true)
            {
                DisplayToDoList(toDoList);
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch ((UserChoice)choice)
                    {
                        case UserChoice.AddTask:
                            AddTask(toDoList);
                            break;
                        case UserChoice.DeleteTask:
                            DeleteTask(toDoList);
                            break;
                        case UserChoice.UpdateTask:
                            UpdateTask(toDoList);
                            break;
                        case UserChoice.SaveToFile:
                            SaveToFile(toDoList);
                            break;
                        case UserChoice.ListAllTasks:
                            ListTasks(toDoList);
                            break;
                        case UserChoice.ListDoneTasks:
                            ListTasksByStatus(toDoList, TaskStatus.Done);
                            break;
                        case UserChoice.ListNotDoneTasks:
                            ListTasksByStatus(toDoList, TaskStatus.NotStarted);
                            break;
                        case UserChoice.ListInProgressTasks:
                            ListTasksByStatus(toDoList, TaskStatus.InProgress);
                            break;
                        case UserChoice.SearchTasks:
                            SearchTasks(toDoList);
                            break;
                        case UserChoice.Exit:
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void DisplayToDoList(List<Task> toDoList)
        {
            Console.Clear();
            if (toDoList.Count > 0)
            {
                Console.WriteLine("To-Do List:");
                for (int i = 0; i < toDoList.Count; i++)
                {
                    string status = toDoList[i].Status.ToString();
                    string priority = toDoList[i].Priority.ToString();
                    string dueDate = toDoList[i].DueDate.HasValue ? toDoList[i].DueDate.Value.ToString("yyyy-MM-dd") : "No due date";
                    Console.WriteLine($"({i + 1}) {toDoList[i].Description} - {status} - Priority: {priority} - Due Date: {dueDate}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("You currently have no tasks in your To-do list.");
                Console.WriteLine();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Delete Task");
            Console.WriteLine("3. Update Task");
            Console.WriteLine("4. Save To File");
            Console.WriteLine("5. List All Tasks");
            Console.WriteLine("6. List Done Tasks");
            Console.WriteLine("7. List Not Done Tasks");
            Console.WriteLine("8. List In Progress Tasks");
            Console.WriteLine("9. Search Tasks");
            Console.WriteLine("10. Exit");
        }

        static void AddTask(List<Task> toDoList)
        {
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter due date (yyyy-MM-dd) or leave blank: ");
            string dueDateInput = Console.ReadLine();
            DateTime? dueDate = string.IsNullOrWhiteSpace(dueDateInput) ? (DateTime?)null : DateTime.Parse(dueDateInput);

            Console.Write("Enter priority (Low, Medium, High): ");
            if (Enum.TryParse(Console.ReadLine(), true, out TaskPriority priority))
            {
                toDoList.Add(new Task(description, TaskStatus.NotStarted, dueDate, priority));
                Console.Clear();
                Console.WriteLine("Task added successfully!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid priority level. Task not added.");
            }
        }

        static void DeleteTask(List<Task> toDoList)
        {
            if (toDoList.Count > 0)
            {
                Console.WriteLine("Enter the number of the task you want to delete: ");
                for (int i = 0; i < toDoList.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) {toDoList[i].Description}");
                }

                if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum > 0 && taskNum <= toDoList.Count)
                {
                    toDoList.RemoveAt(taskNum - 1);
                    Console.Clear();
                    Console.WriteLine("Task deleted successfully!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid task number. Please try again.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No tasks to delete.");
            }
        }

        static void UpdateTask(List<Task> toDoList)
        {
            if (toDoList.Count > 0)
            {
                Console.WriteLine("Enter the number of the task you want to update: ");
                for (int i = 0; i < toDoList.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) {toDoList[i].Description}");
                }

                if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum > 0 && taskNum <= toDoList.Count)
                {
                    Console.Write("Enter the new description: ");
                    string newDescription = Console.ReadLine();

                    Console.Write("Enter the new status (NotStarted, InProgress, Done): ");
                    if (Enum.TryParse<TaskStatus>(Console.ReadLine(), true, out TaskStatus newStatus))
                    {
                        Console.Write("Enter the new due date (yyyy-MM-dd) or leave blank: ");
                        string newDueDateInput = Console.ReadLine();
                        DateTime? newDueDate = string.IsNullOrWhiteSpace(newDueDateInput) ? (DateTime?)null : DateTime.Parse(newDueDateInput);

                        Console.Write("Enter the new priority (Low, Medium, High): ");
                        if (Enum.TryParse(Console.ReadLine(), true, out TaskPriority newPriority))
                        {
                            toDoList[taskNum - 1] = new Task(newDescription, newStatus, newDueDate, newPriority);
                            Console.Clear();
                            Console.WriteLine("Task updated successfully!");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid priority level. Task not updated.");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid status. Task not updated.");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid task number. Please try again.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No tasks to update.");
            }
        }

        static void ListTasks(List<Task> toDoList)
        {
            DisplayToDoList(toDoList);
        }

        static void ListTasksByStatus(List<Task> toDoList, TaskStatus status)
        {
            Console.Clear();
            var tasks = toDoList.Where(task => task.Status == status).ToList();
            if (tasks.Count > 0)
            {
                Console.WriteLine($"{status} Tasks:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) {tasks[i].Description} - Due Date: {(tasks[i].DueDate.HasValue ? tasks[i].DueDate.Value.ToString("yyyy-MM-dd") : "No due date")} - Priority: {tasks[i].Priority}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"No tasks with status {status}.");
                Console.WriteLine();
            }
        }

        static void SearchTasks(List<Task> toDoList)
        {
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine().ToLower();

            var matchingTasks = toDoList
                .Where(task => task.Description.ToLower().Contains(searchTerm))
                .ToList();

            Console.Clear();
            if (matchingTasks.Count > 0)
            {
                Console.WriteLine("Search Results:");
                for (int i = 0; i < matchingTasks.Count; i++)
                {
                    string status = matchingTasks[i].Status.ToString();
                    string priority = matchingTasks[i].Priority.ToString();
                    string dueDate = matchingTasks[i].DueDate.HasValue ? matchingTasks[i].DueDate.Value.ToString("yyyy-MM-dd") : "No due date";
                    Console.WriteLine($"({i + 1}) {matchingTasks[i].Description} - {status} - Priority: {priority} - Due Date: {dueDate}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No tasks found matching the search term.");
                Console.WriteLine();
            }
        }

        static void SaveToFile(List<Task> toDoList)
        {
            string json = JsonSerializer.Serialize(toDoList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("ToDoList.json", json);
            Console.Clear();
            Console.WriteLine("To-do list saved to ToDoList.json successfully!");
        }
    }
}
