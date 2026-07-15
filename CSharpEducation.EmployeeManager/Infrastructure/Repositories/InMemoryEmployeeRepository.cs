using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;
using CSharpEducation.EmployeeManager.Domain.Interfaces;

namespace CSharpEducation.EmployeeManager.Infrastructure.Repositories;

/// <summary>
/// Реализация хранилища сотрудников в оперативной памяти.
/// </summary>
/// <remarks>
/// Данные не сохраняются между запусками приложения.
/// </remarks>
public class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = [];

    /// <inheritdoc />
    public void Add(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_employees.Any(e => e.Id == employee.Id))
            throw new InvalidEmployeeDataException($"Сотрудник с Id={employee.Id} уже существует.", nameof(employee));

        _employees.Add(employee);
    }

    /// <inheritdoc />
    public void Update(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        var index = _employees.FindIndex(e => e.Id == employee.Id);
        if (index < 0)
            throw new EmployeeNotFoundException(employee.Id);

        _employees[index] = employee;
    }

    /// <inheritdoc />
    public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Employee> GetAll() => _employees.AsReadOnly();
}
