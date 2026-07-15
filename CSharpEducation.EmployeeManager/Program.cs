using CSharpEducation.EmployeeManager.Application.Services;
using CSharpEducation.EmployeeManager.Domain.Exceptions;
using CSharpEducation.EmployeeManager.Infrastructure.Repositories;
using CSharpEducation.EmployeeManager.Infrastructure.Services;
using CSharpEducation.EmployeeManager.Presentation;

try
{
    var repository = new InMemoryEmployeeRepository();
    var salaryCalculator = new SalaryCalculator();
    var employeeService = new EmployeeService(repository, salaryCalculator);
    var menu = new ConsoleMenu(employeeService);

    menu.Run();
}
catch (EmployeeManagerException ex)
{
    Console.WriteLine($"Критическая ошибка приложения: {ex.Message}");
    Environment.ExitCode = 1;
}
catch (Exception ex)
{
    Console.WriteLine($"Необработанное исключение: {ex.Message}");
    Environment.ExitCode = 1;
}
