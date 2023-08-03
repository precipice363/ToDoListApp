using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ToDoListApp
{
    public class Task
    {
        public int Id {get; set; }
        public string Title {get; set; }
        public bool isCompleted {get; set; }
    }

    class Program
    {
        private static List<Task> tasks = new List<Task>();
        private static string dataFilePath = "tasks.json";

        static void Main(string[] args)
        {
            LoadTasksFromJson();

            while (true)
            {
                Console.WriteLine("To-Do List App");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Mark Task as Completed");
                Console.WriteLine("4. Save and Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    AddTask();
                    break;
                    case "2":
                    ViewTasks();
                    break;
                    case "3":
                    MarkTaskCompleted();
                    break;
                    case "4":
                    SaveTasksToJson();
                    return;
                    default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Task task = new Task
            {
                Id = tasks.Count + 1,
                Title = title,
                isCompleted = false
            };

            tasks.Add(task);
            Console.WriteLine("Tasks added successfully!");
        }

        static void ViewTasks()
        {
            Console.WriteLine("Tasks: ");

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id}. {task.Title} - {(task.isCompleted ? "Completed" : "Not Completed")}");
            }
        }

        static void MarkTaskCompleted()
        {
            Console.Write("Enter the task ID to mark as completed: ");

            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.Find(task => task.Id == taskId);
                if (task != null)
                {
                    task.isCompleted = true;
                    Console.WriteLine("Task markes as completed!");
                }
                else{
                    Console.WriteLine("Task not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid task ID!");
            }
        }

        static void LoadTasksFromJson()
        {
            if (File.Exists(dataFilePath))
            {
                string jsonData = File.ReadAllText(dataFilePath);
                tasks = JsonConvert.DeserializeObject<List<Task>>(jsonData);
            }
        }

        static void SaveTasksToJson()
        {
            string jsonData = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(dataFilePath, jsonData);
            Console.WriteLine("Tasks saved to file!");
        }
    }
}
