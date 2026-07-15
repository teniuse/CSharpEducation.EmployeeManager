namespace CSharpEducation.EmployeeManager.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда сотрудник с указанным идентификатором не найден.
/// </summary>
public sealed class EmployeeNotFoundException : EmployeeManagerException
{
    /// <summary>
    /// Идентификатор сотрудника, который не удалось найти.
    /// </summary>
    public int EmployeeId { get; }

    /// <summary>
    /// Создаёт исключение для указанного идентификатора сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор отсутствующего сотрудника.</param>
    public EmployeeNotFoundException(int employeeId)
        : base($"Сотрудник с Id={employeeId} не найден.")
    {
        EmployeeId = employeeId;
    }
}
