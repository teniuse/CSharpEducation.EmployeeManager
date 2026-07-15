using CSharpEducation.EmployeeManager.Application.Interfaces;
using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;
using CSharpEducation.EmployeeManager.Domain.Interfaces;

namespace CSharpEducation.EmployeeManager.Application.Services;

/// <summary>
/// Реализация прикладного сервиса управления сотрудниками.
/// </summary>
/// <remarks>
/// Инкапсулирует валидацию, работу с хранилищем и расчёт зарплаты.
/// Инфраструктурные исключения, не относящиеся к домену, оборачиваются в <see cref="EmployeeManagerException"/>.
/// </remarks>
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly ISalaryCalculator _salaryCalculator;
    private int _nextId = 1;

    /// <summary>
    /// Создаёт экземпляр сервиса сотрудников.
    /// </summary>
    /// <param name="repository">Хранилище сотрудников.</param>
    /// <param name="salaryCalculator">Калькулятор зарплаты.</param>
    /// <exception cref="ArgumentNullException">Если один из зависимостей равен <see langword="null"/>.</exception>
    public EmployeeService(IEmployeeRepository repository, ISalaryCalculator salaryCalculator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _salaryCalculator = salaryCalculator ?? throw new ArgumentNullException(nameof(salaryCalculator));
    }

    /// <inheritdoc />
    public Employee Add(string fullName, string position, decimal baseSalary, decimal bonusPercent)
    {
        Validate(fullName, position, baseSalary, bonusPercent);

        var employee = new Employee
        {
            Id = _nextId++,
            FullName = fullName.Trim(),
            Position = position.Trim(),
            BaseSalary = baseSalary,
            BonusPercent = bonusPercent
        };

        try
        {
            _repository.Add(employee);
        }
        catch (Exception ex) when (ex is not EmployeeManagerException)
        {
            throw new EmployeeManagerException($"Не удалось добавить сотрудника: {ex.Message}", ex);
        }

        return employee;
    }

    /// <inheritdoc />
    public void Update(int id, string fullName, string position, decimal baseSalary, decimal bonusPercent)
    {
        Validate(fullName, position, baseSalary, bonusPercent);

        var employee = GetRequiredEmployee(id);

        employee.FullName = fullName.Trim();
        employee.Position = position.Trim();
        employee.BaseSalary = baseSalary;
        employee.BonusPercent = bonusPercent;

        try
        {
            _repository.Update(employee);
        }
        catch (EmployeeNotFoundException)
        {
            throw;
        }
        catch (Exception ex) when (ex is not EmployeeManagerException)
        {
            throw new EmployeeManagerException($"Не удалось обновить сотрудника Id={id}: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public Employee GetById(int id) => GetRequiredEmployee(id);

    /// <inheritdoc />
    public IReadOnlyList<Employee> GetAll() => _repository.GetAll();

    /// <inheritdoc />
    public decimal CalculateSalary(int id)
    {
        var employee = GetRequiredEmployee(id);

        try
        {
            return _salaryCalculator.Calculate(employee);
        }
        catch (InvalidEmployeeDataException)
        {
            throw;
        }
        catch (Exception ex) when (ex is not EmployeeManagerException)
        {
            throw new EmployeeManagerException(
                $"Не удалось рассчитать зарплату для сотрудника Id={id}: {ex.Message}",
                ex);
        }
    }

    /// <summary>
    /// Возвращает сотрудника по Id или выбрасывает исключение, если он не найден.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns>Найденный сотрудник.</returns>
    /// <exception cref="InvalidEmployeeDataException">Если Id меньше или равен нулю.</exception>
    /// <exception cref="EmployeeNotFoundException">Если сотрудник отсутствует в хранилище.</exception>
    private Employee GetRequiredEmployee(int id)
    {
        if (id <= 0)
            throw new InvalidEmployeeDataException("Id сотрудника должен быть положительным числом.", nameof(id));

        return _repository.GetById(id) ?? throw new EmployeeNotFoundException(id);
    }

    /// <summary>
    /// Проверяет корректность данных сотрудника перед сохранением.
    /// </summary>
    /// <param name="fullName">ФИО.</param>
    /// <param name="position">Должность.</param>
    /// <param name="baseSalary">Оклад.</param>
    /// <param name="bonusPercent">Процент премии.</param>
    /// <exception cref="InvalidEmployeeDataException">Если одно из значений некорректно.</exception>
    private static void Validate(string fullName, string position, decimal baseSalary, decimal bonusPercent)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new InvalidEmployeeDataException("ФИО не может быть пустым.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(position))
            throw new InvalidEmployeeDataException("Должность не может быть пустой.", nameof(position));

        if (baseSalary < 0)
            throw new InvalidEmployeeDataException("Оклад не может быть отрицательным.", nameof(baseSalary));

        if (bonusPercent < 0)
            throw new InvalidEmployeeDataException("Процент премии не может быть отрицательным.", nameof(bonusPercent));

        if (bonusPercent > 100)
            throw new InvalidEmployeeDataException("Процент премии не может превышать 100.", nameof(bonusPercent));
    }
}
