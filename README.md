# TaskTracker

TaskTracker is a console application developed in C# (.NET 8) for personal task management. It allows you to create, list, update, delete, and change the status of tasks, storing the information in a `tasks.json` file.

## Features

- **Add tasks**: Create new tasks with a description and initial "todo" status.
- **List tasks**: Show all tasks or filter by status (`todo`, `inprogress`, `done`).
- **Update tasks**: Modify the description of an existing task.
- **Delete tasks**: Remove tasks by their identifier.
- **Change status**: Mark tasks as "in progress" or "done".
- **Persistence**: All tasks are stored in a local JSON file.

## Available commands

- `add <description>`: Add a new task.
- `list`: List all tasks.
- `list-todo`: List only pending tasks.
- `list-in-progress`: List tasks in progress.
- `list-done`: List completed tasks.
- `update <id> <description>`: Update the description of a task.
- `delete <id>`: Delete a task by its ID.
- `mark-in-progress <id>`: Mark a task as "in progress".
- `mark-done <id>`: Mark a task as "done".

## Project URL
https://roadmap.sh/projects/task-tracker

## Installation and usage

1. **Requirements**:  
   - .NET 8 SDK  
   - Operating system compatible with .NET

2. **Clone the repository**:
   git clone <https://github.com/Sebascode20/TaskTracker.git> cd TaskTracker

3. **Build the application**:
   dotnet build

4. **Run the application**:
  dotnet run -- <command> [arguments]
  Example: dotnet run -- add "Buy milk"

## Project structure

- `TaskItem.cs`: Main logic for task management and commands.
- `tasks.json`: File where tasks are stored.
- `Program.cs`: Application entry point.

## Notes

- The possible task statuses are: `todo`, `inprogress`, `done`.
   
