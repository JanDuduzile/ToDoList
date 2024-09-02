To-Do List Application
Overview
This is a command-line based to-do list application developed in C#. It allows users to manage their tasks efficiently with features including task addition, deletion, updating, and viewing. The application supports due dates, priority levels, and task statuses. Additionally, it includes search functionality to help users quickly find tasks based on keywords.

Features
Task Management: Add, delete, and update tasks with descriptions.
Task Status: Track tasks with statuses such as Not Started, In Progress, and Done.
Due Dates: Assign and view due dates for tasks.
Priority Levels: Set and view priority levels (Low, Medium, High) for tasks.
Task Listing: List all tasks or filter tasks based on their status.
Search Functionality: Search for tasks using keywords in their descriptions.
File Saving: Save the to-do list to a JSON file for persistence.
Getting Started
Prerequisites
.NET SDK (version 5.0 or higher)
Installation
Clone the repository:

bash
Copy code
git clone https://github.com/JanDuduzile/to-do-list-app.git
Navigate to the project directory:

bash
Copy code
cd to-do-list-app
Build and run the application:

bash
Copy code
dotnet run
Usage
When the application starts, you'll be presented with a menu with the following options:

Add Task: Enter a task description, due date, and priority level.
Delete Task: Remove a task from the list by specifying its number.
Update Task: Modify a task's description, status, due date, or priority.
Save To File: Save the current to-do list to a ToDoList.json file.
List All Tasks: View all tasks with their statuses, priorities, and due dates.
List Done Tasks: View only tasks marked as Done.
List Not Done Tasks: View only tasks that are Not Started.
List In Progress Tasks: View only tasks marked as In Progress.
Search Tasks: Find tasks by entering keywords from the task descriptions.
Exit: Close the application.


License
This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgements
Roadmap.sh Back-End projects
.NET SDK for development.
System.Text.Json for JSON serialization.
