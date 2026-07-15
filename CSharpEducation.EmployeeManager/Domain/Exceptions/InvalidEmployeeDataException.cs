namespace CSharpEducation.EmployeeManager.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при некорректных данных сотрудника.
/// </summary>
public sealed class InvalidEmployeeDataException : EmployeeManagerException
{
    /// <summary>
    /// Имя параметра, значение которого некорректно; может быть <see langword="null"/>.
    /// </summary>
    public string? ParameterName { get; }

    /// <summary>
    /// Создаёт исключение с описанием ошибки валидации.
    /// </summary>
    /// <param name="message">Текст ошибки.</param>
    /// <param name="parameterName">Имя некорректного параметра.</param>
    public InvalidEmployeeDataException(string message, string? parameterName = null)
        : base(message)
    {
        ParameterName = parameterName;
    }
}
