using System.CommandLine;
using System.Threading.Tasks;

namespace TaskTracker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TaskItem task1 = new TaskItem();

            var rootCommand = new RootCommand("Task Tracker CLI");

            rootCommand.AddCommand(task1.List());

            rootCommand.AddCommand(task1.Add());

            rootCommand.AddCommand(task1.Update());

            rootCommand.AddCommand(task1.Delete());

            rootCommand.AddCommand(task1.MarkInProgress());

            rootCommand.AddCommand(task1.MarkDone());

            rootCommand.AddCommand(task1.ListTaskDone());

            rootCommand.AddCommand(task1.ListTaskTodo());

            rootCommand.AddCommand(task1.ListTaskInProgress());

            await rootCommand.InvokeAsync(args);
        }
    }
}
