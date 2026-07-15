using CSharpEducation.EmployeeManager.Domain.Entities;
using CSharpEducation.EmployeeManager.Domain.Exceptions;
using CSharpEducation.EmployeeManager.Domain.Interfaces;

namespace CSharpEducation.EmployeeManager.Infrastructure.Services;

/// <summary>
/// Калькулятор зарплаты: оклад плюс процентная премия.
/// </summary>
/// <remarks>
/// Формула расчёта: <c>BaseSalary + BaseSalary * BonusPercent / 100</c>,
/// результат округляется до двух знаков после запятой.
/// </remarks>
public class SalaryCalculator : ISalaryCalculator
{
    /// <inheritdoc />
    public decimal Calculate(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (employee.BaseSalary < 0)
            throw new InvalidEmployeeDataException("Невозможно рассчитать зарплату: оклад отрицательный.");

        if (employee.BonusPercent < 0)
            throw new InvalidEmployeeDataException("Невозможно рассчитать зарплату: процент премии отрицательный.");

        var bonus = employee.BaseSalary * employee.BonusPercent / 100m;
        return Math.Round(employee.BaseSalary + bonus, 2);
    }
}
