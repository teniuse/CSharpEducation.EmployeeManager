using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;

namespace CSharpEducation.EmployeeManager.Domain.Interfaces;

/// <summary>
/// Контракт расчёта заработной платы сотрудника.
/// </summary>
public interface ISalaryCalculator
{
    /// <summary>
    /// Рассчитывает итоговую зарплату сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник, для которого выполняется расчёт.</param>
    /// <returns>Сумма зарплаты с учётом премии, округлённая до двух знаков.</returns>
    /// <exception cref="ArgumentNullException">Если <paramref name="employee"/> равен <see langword="null"/>.</exception>
    /// <exception cref="InvalidEmployeeDataException">Если данные сотрудника некорректны для расчёта.</exception>
    decimal Calculate(Employee employee);
}
