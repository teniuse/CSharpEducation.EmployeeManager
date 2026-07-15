namespace CSharpEducation.EmployeeManager.Domain.Exceptions;

/// <summary>
/// Базовое исключение приложения управления сотрудниками.
/// </summary>
/// <remarks>
/// Используется как общий тип для перехвата доменных и прикладных ошибок приложения,
/// а также для обёртки инфраструктурных исключений с сохранением <see cref="Exception.InnerException"/>.
/// </remarks>
public class EmployeeManagerException : Exception
{
    /// <summary>
    /// Создаёт исключение с указанным сообщением.
    /// </summary>
    /// <param name="message">Текст ошибки.</param>
    public EmployeeManagerException(string message) : base(message)
    {
    }

    /// <summary>
    /// Создаёт исключение с сообщением и внутренним исключением.
    /// </summary>
    /// <param name="message">Текст ошибки.</param>
    /// <param name="innerException">Исходное исключение, ставшее причиной ошибки.</param>
    public EmployeeManagerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
