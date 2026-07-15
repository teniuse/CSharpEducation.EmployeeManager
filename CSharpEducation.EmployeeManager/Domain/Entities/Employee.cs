namespace CSharpEducation.EmployeeManager.Domain.Entities;

/// <summary>
/// Доменная сущность сотрудника.
/// </summary>
public class Employee
{
    /// <summary>
    /// Уникальный идентификатор сотрудника.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество сотрудника.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Должность сотрудника.
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Базовый оклад сотрудника.
    /// </summary>
    public decimal BaseSalary { get; set; }

    /// <summary>
    /// Процент премии к окладу (от 0 до 100).
    /// </summary>
    public decimal BonusPercent { get; set; }
}
