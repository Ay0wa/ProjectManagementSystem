using ProjectManagementSystem.Application.Commands;
using ProjectManagementSystem.Application.Queries;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementSystem
{
    class Program
    {
        private static readonly List<Project> projects = new();
        private static readonly List<Problem> problems = new();
        private static readonly CreateProjectCommandHandler createProjectHandler;
        private static readonly UpdateProjectCommandHandler updateProjectHandler;
        private static readonly CreateProblemCommandHandler createProblemHandler;
        private static readonly UpdateProblemCommandHandler updateProblemHandler;
        private static readonly GetProjectsQueryHandler getProjectsHandler;
        private static readonly GetProblemsQueryHandler getProblemsHandler;

        static Program()
        {
            createProjectHandler = new CreateProjectCommandHandler(projects);
            updateProjectHandler = new UpdateProjectCommandHandler(projects);
            createProblemHandler = new CreateProblemCommandHandler(projects);
            updateProblemHandler = new UpdateProblemCommandHandler(projects); 
            getProjectsHandler = new GetProjectsQueryHandler(projects);
            getProblemsHandler = new GetProblemsQueryHandler(problems);
        }

        static void Main(string[] args)
        {            
            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProject();
                        break;
                    case "2":
                        CreateProblem();
                        break;
                    case "3":
                        ShowProjects();
                        break;
                    case "4":
                        UpdateProject();
                        break;
                    case "5":
                        UpdateProblem();
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n=== Система управления проектами ===");
            Console.WriteLine("1. Создать проект");
            Console.WriteLine("2. Создать задачу");
            Console.WriteLine("3. Показать проекты");
            Console.WriteLine("4. Отредактировать проект");
            Console.WriteLine("5. Обновить статус задачи");
            Console.WriteLine("q. Выход");
            Console.Write("\nВыберите действие: ");
        }

        static void CreateProject()
        {
            Console.WriteLine("\n=== Создание проекта ===");
            
            Console.Write("Введите название проекта: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите описание проекта: ");
            var description = Console.ReadLine() ?? string.Empty;

            var command = new CreateProjectCommand
            {
                Name = name,
                Description = description,
                CreatedBy = "User" // В реальной системе здесь был бы текущий пользователь
            };

            createProjectHandler.Handle(command);
            Console.WriteLine("Проект успешно создан!");
        }

        static void UpdateProject()
        {
            var projects = getProjectsHandler.Handle(new GetProjectsQuery());
            if (!projects.Any())
            {
                Console.WriteLine("Сначала создайте хотя бы один проект!");
                return;
            }

            Console.WriteLine("\nДоступные проекты:");
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Название: {project.Name}");
            }

            Console.WriteLine("\n=== Обновление проекта ===");
            
            Console.Write("\nВведите ID проекта, который хотите обновить: ");
            var projectIdString = Console.ReadLine();
            if (!Guid.TryParse(projectIdString, out Guid projectId))
            {
                Console.WriteLine("Неверный формат ID проекта!");
                return;
            }

            Console.Write("Введите новое название проекта: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите новое описание проекта: ");
            var description = Console.ReadLine() ?? string.Empty;

            var command = new UpdateProjectCommand
            {
                ProjectId = projectId, 
                Name = name,
                Description = description,
                CreatedBy = "User"
            };

            updateProjectHandler.Handle(command);
        }

        static void CreateProblem()
        {
            var projects = getProjectsHandler.Handle(new GetProjectsQuery());
            if (!projects.Any())
            {
                Console.WriteLine("Сначала создайте хотя бы один проект!");
                return;
            }

            Console.WriteLine("\n=== Создание задачи ===");
            
            Console.WriteLine("\nДоступные проекты:");
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Название: {project.Name}");
            }

            Console.Write("\nВведите ID проекта: ");
            var projectIdString = Console.ReadLine();
            if (!Guid.TryParse(projectIdString, out Guid projectId))
            {
                Console.WriteLine("Неверный формат ID проекта!");
                return;
            }

            Console.Write("Введите название задачи: ");
            var title = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите описание задачи: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Выберите приоритет (0 - Low, 1 - Medium, 2 - High, 3 - Critical): ");
            var priorityString = Console.ReadLine();
            if (!Enum.TryParse(priorityString, out Priority priority))
            {
                Console.WriteLine("Неверный приоритет!");
                return;
            }

            var command = new CreateProblemCommand
            {
                ProjectId = projectId,
                Title = title,
                Description = description,
                Priority = priority,
                CreatedBy = "User" // В реальной системе здесь был бы текущий пользователь
            };

            try
            {
                createProblemHandler.Handle(command);
                Console.WriteLine("Задача успешно создана!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void UpdateProblem()
        {
            var projects = getProjectsHandler.Handle(new GetProjectsQuery());
            if (!projects.Any())
            {
                Console.WriteLine("Нет доступных проектов!");
                return;
            }

            Console.WriteLine("\n=== Обновление задачи ===");

            // Показываем все проекты и их задачи
            foreach (var project in projects)
            {
                Console.WriteLine($"\nПроект: {project.Name} (ID: {project.Id})");
                if (project.Problems.Any())
                {
                    Console.WriteLine("Задачи:");
                    foreach (var Problem in project.Problems)
                    {
                        Console.WriteLine($"- ID: {Problem.Id}, Название: {Problem.Title}, Статус: {Problem.Status}, Приоритет: {Problem.Priority}");
                    }
                }
                else
                {
                    Console.WriteLine("В этом проекте нет задач.");
                }
            }

            // Получаем ID задачи для обновления
            Console.Write("\nВведите ID задачи для обновления: ");
            var ProblemIdString = Console.ReadLine();
            if (!Guid.TryParse(ProblemIdString, out Guid ProblemId))
            {
                Console.WriteLine("Неверный формат ID задачи!");
                return;
            }

            Console.WriteLine("Выберите новый статус (0 - New, 1 - InProgress, 2 - Completed), или нажмите Enter, чтобы не менять: ");
            var statusString = Console.ReadLine();
            ProblemState? status = null;
            if (!string.IsNullOrWhiteSpace(statusString) && Enum.TryParse(statusString, out ProblemState parsedStatus))
            {
                status = parsedStatus;
            }

            var command = new UpdateProblemCommand
            {
                ProblemId = ProblemId,
                State = status,
            };

            try
            {
                updateProblemHandler.Handle(command);
                Console.WriteLine("Задача успешно обновлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ShowProjects()
        {
            var projects = getProjectsHandler.Handle(new GetProjectsQuery());
            
            Console.WriteLine("\n=== Список проектов ===");
            if (!projects.Any())
            {
                Console.WriteLine("Проектов пока нет.");
                return;
            }

            foreach (var project in projects)
            {
                Console.WriteLine($"\nПроект: {project.Name}");
                Console.WriteLine($"Описание: {project.Description}");
                Console.WriteLine($"Создан: {project.CreatedAt} пользователем {project.CreatedBy}");
                
                if (project.Problems.Any())
                {
                    Console.WriteLine("Задачи:");
                    foreach (var Problem in project.Problems)
                    {
                        Console.WriteLine($"- {Problem.Title} ({Problem.Status}) - {Problem.Priority}");
                    }
                }
                else
                {
                    Console.WriteLine("Задач пока нет.");
                }
                Console.WriteLine("------------------------");
            }
        }
    }
}