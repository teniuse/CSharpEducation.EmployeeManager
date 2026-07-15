using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;

namespace CSharpEducation.EmployeeManager.Domain.Interfaces;

/// <summary>
/// Контракт хранилища сотрудников.
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Добавляет сотрудника в хранилище.
    /// </summary>
    /// <param name="employee">Сотрудник для добавления.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="employee"/> равен <see langword="null"/>.</exception>
    /// <exception cref="InvalidEmployeeDataException">Если сотрудник с таким Id уже существует.</exception>
    void Add(Employee employee);

    /// <summary>
    /// Обновляет данные существующего сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник с обновлёнными данными.</param>
    /// <exception cref="ArgumentNullException">Если <paramref name="employee"/> равен <see langword="null"/>.</exception>
    /// <exception cref="EmployeeNotFoundException">Если сотрудник с указанным Id не найден.</exception>
    void Update(Employee employee);

    /// <summary>
    /// Возвращает сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns>Сотрудник или <see langword="null"/>, если не найден.</returns>
    Employee? GetById(int id);

    /// <summary>
    /// Возвращает всех сотрудников из хранилища.
    /// </summary>
    /// <returns>Только для чтения список сотрудников.</returns>
    IReadOnlyList<Employee> GetAll();
}
