using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;

namespace CSharpEducation.EmployeeManager.Application.Interfaces;

/// <summary>
/// Сервис прикладного уровня для управления сотрудниками и расчёта зарплаты.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Добавляет нового сотрудника.
    /// </summary>
    /// <param name="fullName">ФИО сотрудника.</param>
    /// <param name="position">Должность.</param>
    /// <param name="baseSalary">Базовый оклад.</param>
    /// <param name="bonusPercent">Процент премии (0–100).</param>
    /// <returns>Созданный сотрудник с назначенным идентификатором.</returns>
    /// <exception cref="InvalidEmployeeDataException">Если переданные данные некорректны.</exception>
    /// <exception cref="EmployeeManagerException">Если сохранение завершилось инфраструктурной ошибкой.</exception>
    Employee Add(string fullName, string position, decimal baseSalary, decimal bonusPercent);

    /// <summary>
    /// Обновляет данные существующего сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="fullName">Новое ФИО.</param>
    /// <param name="position">Новая должность.</param>
    /// <param name="baseSalary">Новый оклад.</param>
    /// <param name="bonusPercent">Новый процент премии (0–100).</param>
    /// <exception cref="InvalidEmployeeDataException">Если переданные данные или Id некорректны.</exception>
    /// <exception cref="EmployeeNotFoundException">Если сотрудник не найден.</exception>
    /// <exception cref="EmployeeManagerException">Если обновление завершилось инфраструктурной ошибкой.</exception>
    void Update(int id, string fullName, string position, decimal baseSalary, decimal bonusPercent);

    /// <summary>
    /// Возвращает сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns>Найденный сотрудник.</returns>
    /// <exception cref="EmployeeNotFoundException">Если сотрудник не найден.</exception>
    /// <exception cref="InvalidEmployeeDataException">Если Id некорректен.</exception>
    Employee GetById(int id);

    /// <summary>
    /// Возвращает всех сотрудников.
    /// </summary>
    /// <returns>Только для чтения список сотрудников.</returns>
    IReadOnlyList<Employee> GetAll();

    /// <summary>
    /// Рассчитывает зарплату сотрудника по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <returns>Итоговая зарплата с учётом премии.</returns>
    /// <exception cref="EmployeeNotFoundException">Если сотрудник не найден.</exception>
    /// <exception cref="InvalidEmployeeDataException">Если данные сотрудника некорректны для расчёта.</exception>
    /// <exception cref="EmployeeManagerException">Если расчёт завершился инфраструктурной ошибкой.</exception>
    decimal CalculateSalary(int id);
}
