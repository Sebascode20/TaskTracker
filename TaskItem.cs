using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CommandLine;
using System.Text.Json;
using System.IO;

namespace TaskTracker
{
    internal class TaskItem
    {
        public int id { get; set; }

        public string description { get; set; } 

        public Status status { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public Command Add()
        {
            var addTaskCommand = new Command("add", "Add task");

            var descriptionArg = new Argument<string>("description", "taskDescription");

            addTaskCommand.Add(descriptionArg);

            addTaskCommand.SetHandler((string description) =>
            {
                var task = new TaskItem()
                {
                    id = generateId(),
                    description = description,
                    status = Status.todo,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };

                var tasks = loadTasks();
                tasks.Add(task);
                SaveTasks(tasks);

                Console.WriteLine($"Task added successfully (ID: {id})");
            }, descriptionArg);

            return addTaskCommand; 
        }

        private static int generateId()
        {
            var tasks = loadTasks();
            return tasks.Count > 0 ? tasks.Max(tasks => tasks.id) + 1 : 1;
        }

        private static List<TaskItem> loadTasks()
        {
            var file = "tasks.json";

            if (!File.Exists(file)) return new List<TaskItem>();

            var json = File.ReadAllText(file);

            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private static void SaveTasks(List<TaskItem> tasks)
        {
            var file = "tasks.json";

            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(file, json);
        }
    }

    enum Status
    {
        todo,
        inprogress,
        done
    }
}
