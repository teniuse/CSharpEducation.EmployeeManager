using CSharpEducation.EmployeeManager.Application.Interfaces;
using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;

namespace CSharpEducation.EmployeeManager.Presentation;

/// <summary>
/// Консольное меню приложения управления сотрудниками.
/// </summary>
public class ConsoleMenu
{
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Создаёт консольное меню.
    /// </summary>
    /// <param name="employeeService">Сервис управления сотрудниками.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="employeeService"/> равен <see langword="null"/>.</exception>
    public ConsoleMenu(IEmployeeService employeeService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    /// <summary>
    /// Запускает основной цикл меню до выбора пункта «Выход».
    /// </summary>
    public void Run()
    {
        while (true)
        {
            PrintMenu();
            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        UpdateEmployee();
                        break;
                    case "3":
                        ShowEmployee();
                        break;
                    case "4":
                        ShowAllEmployees();
                        break;
                    case "5":
                        CalculateSalary();
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню. Попробуйте снова.");
                        break;
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine($"[Не найдено] {ex.Message}");
            }
            catch (InvalidEmployeeDataException ex)
            {
                var details = string.IsNullOrEmpty(ex.ParameterName)
                    ? ex.Message
                    : $"{ex.Message} (параметр: {ex.ParameterName})";
                Console.WriteLine($"[Некорректные данные] {details}");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"[Ошибка ввода] {ex.Message}");
            }
            catch (EmployeeManagerException ex)
            {
                Console.WriteLine($"[Ошибка приложения] {ex.Message}");
                if (ex.InnerException is not null)
                    Console.WriteLine($"  Причина: {ex.InnerException.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"[Внутренняя ошибка] Отсутствует обязательный аргумент: {ex.ParamName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Непредвиденная ошибка] {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Выводит пункты главного меню.
    /// </summary>
    private static void PrintMenu()
    {
        Console.WriteLine("---[ Управление сотрудниками ]---");
        Console.WriteLine("1. Добавить сотрудника");
        Console.WriteLine("2. Обновить сотрудника");
        Console.WriteLine("3. Получить информацию о сотруднике");
        Console.WriteLine("4. Показать всех сотрудников");
        Console.WriteLine("5. Рассчитать зарплату");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите пункт: ");
    }

    /// <summary>
    /// Запрашивает данные и добавляет нового сотрудника.
    /// </summary>
    private void AddEmployee()
    {
        Console.Write("ФИО: ");
        var fullName = Console.ReadLine() ?? string.Empty;

        Console.Write("Должность: ");
        var position = Console.ReadLine() ?? string.Empty;

        var baseSalary = ReadDecimal("Оклад: ");
        var bonusPercent = ReadDecimal("Процент премии: ");

        var employee = _employeeService.Add(fullName, position, baseSalary, bonusPercent);
        Console.WriteLine($"Сотрудник добавлен. Id={employee.Id}");
    }

    /// <summary>
    /// Проверяет существование сотрудника по Id, затем запрашивает и сохраняет новые данные.
    /// </summary>
    private void UpdateEmployee()
    {
        var id = ReadInt("Id сотрудника: ");
        var existing = _employeeService.GetById(id);

        Console.WriteLine("Текущие данные:");
        PrintEmployee(existing);

        Console.Write("ФИО: ");
        var fullName = Console.ReadLine() ?? string.Empty;

        Console.Write("Должность: ");
        var position = Console.ReadLine() ?? string.Empty;

        var baseSalary = ReadDecimal("Оклад: ");
        var bonusPercent = ReadDecimal("Процент премии: ");

        _employeeService.Update(id, fullName, position, baseSalary, bonusPercent);
        Console.WriteLine("Данные сотрудника обновлены.");
    }

    /// <summary>
    /// Выводит информацию о сотруднике по Id.
    /// </summary>
    private void ShowEmployee()
    {
        var id = ReadInt("Id сотрудника: ");
        var employee = _employeeService.GetById(id);
        PrintEmployee(employee);
    }

    /// <summary>
    /// Выводит список всех сотрудников.
    /// </summary>
    private void ShowAllEmployees()
    {
        var employees = _employeeService.GetAll();
        if (employees.Count == 0)
        {
            Console.WriteLine("Список сотрудников пуст.");
            return;
        }

        foreach (var employee in employees)
            PrintEmployee(employee);
    }

    /// <summary>
    /// Рассчитывает и выводит зарплату сотрудника по Id.
    /// </summary>
    private void CalculateSalary()
    {
        var id = ReadInt("Id сотрудника: ");
        var salary = _employeeService.CalculateSalary(id);
        Console.WriteLine($"Зарплата сотрудника Id={id}: {salary:C}");
    }

    /// <summary>
    /// Выводит данные сотрудника в консоль.
    /// </summary>
    /// <param name="employee">Сотрудник для отображения.</param>
    private static void PrintEmployee(Employee employee)
    {
        Console.WriteLine(
            $"Id={employee.Id}, ФИО={employee.FullName}, Должность={employee.Position}, " +
            $"Оклад={employee.BaseSalary:C}, Премия={employee.BonusPercent}%");
    }

    /// <summary>
    /// Считывает целое число из консоли.
    /// </summary>
    /// <param name="prompt">Текст приглашения к вводу.</param>
    /// <returns>Распознанное целое число.</returns>
    /// <exception cref="InvalidInputException">Если ввод не является целым числом.</exception>
    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var value))
            throw new InvalidInputException($"Ожидалось целое число, получено: '{input}'.");

        return value;
    }

    /// <summary>
    /// Считывает десятичное число из консоли.
    /// </summary>
    /// <param name="prompt">Текст приглашения к вводу.</param>
    /// <returns>Распознанное десятичное число.</returns>
    /// <exception cref="InvalidInputException">Если ввод не является числом.</exception>
    private static decimal ReadDecimal(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (!decimal.TryParse(input, out var value))
            throw new InvalidInputException($"Ожидалось число, получено: '{input}'.");

        return value;
    }
}
