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

        public Command List()
        {
            var listTaskCommand = new Command("list", "List tasks");

            listTaskCommand.SetHandler(() =>
            {
                var tasks = loadTasks();

                foreach (var item in tasks)
                {
                    Console.WriteLine(item.ToString());
                }
            });

            return listTaskCommand;
        }

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

        public Command Update()
        {
            var updateTaskCommand = new Command("update", "update task");

            var idArg = new Argument<int>("id", "taskId");

            var descriptionArg = new Argument<string>("description", "taskDescription");

            var statusArg = new Argument<Status>("status", "taskStatus");

            updateTaskCommand.Add(idArg);

            updateTaskCommand.Add(descriptionArg);

            updateTaskCommand.SetHandler((int id, string description) =>
            {
                string jsonString = File.ReadAllText("tasks.json");

                List<TaskItem> userTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

                TaskItem updatedTask = userTasks.FirstOrDefault(userTasks => userTasks.id == id);

                if (updatedTask == null) Console.WriteLine($"Task with id {id} not found");

                else
                {
                    updatedTask.description = description;

                    updatedTask.updatedAt = DateTime.Now;
                }

                // Serializar el objeto actualizado a JSON
                string newJsonString = JsonSerializer.Serialize(userTasks, new JsonSerializerOptions { WriteIndented = true });

                // Sobrescribir el archivo JSON
                File.WriteAllText("tasks.json", newJsonString);

            }, idArg, descriptionArg);

            return updateTaskCommand;
        }

        public Command Delete()
        {
            var deleteTaskCommand = new Command("delete", "delete task");

            var idArg = new Argument<int>("id", "taskId");

            deleteTaskCommand.Add(idArg);

            deleteTaskCommand.SetHandler((int id) =>
            {
                string jsonString = File.ReadAllText("tasks.json");

                List<TaskItem> userTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

                TaskItem deletedTask = userTasks.FirstOrDefault(userTasks => userTasks.id == id);

                if (deletedTask == null) Console.WriteLine($"Task with id {id} not found");

                else userTasks.Remove(deletedTask);

                // Serializar el objeto actualizado a JSON
                string newJsonString = JsonSerializer.Serialize(userTasks, new JsonSerializerOptions { WriteIndented = true });

                // Sobrescribir el archivo JSON
                File.WriteAllText("tasks.json", newJsonString);
            }, idArg);

            return deleteTaskCommand;
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

        public override string ToString()
        {
            return $"ID: {id}, Descripción: {description}, Estado: {status}, Creado: {createdAt}, Actualizado: {updatedAt}";
        }
    }

    enum Status
    {
        todo,
        inprogress,
        done
    }
}
