namespace CSharpEducation.EmployeeManager.Domain.Exceptions;

/// <summary>
/// Исключение, возникающее при некорректном пользовательском вводе в консоли.
/// </summary>
public sealed class InvalidInputException : EmployeeManagerException
{
    /// <summary>
    /// Создаёт исключение с указанным сообщением.
    /// </summary>
    /// <param name="message">Текст ошибки ввода.</param>
    public InvalidInputException(string message) : base(message)
    {
    }

    /// <summary>
    /// Создаёт исключение с сообщением и внутренним исключением.
    /// </summary>
    /// <param name="message">Текст ошибки ввода.</param>
    /// <param name="innerException">Исходное исключение.</param>
    public InvalidInputException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
