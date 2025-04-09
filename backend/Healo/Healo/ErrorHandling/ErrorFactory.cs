namespace Healo.ErrorHandling;

public static class ErrorFactory
{
    public static Error CreateError(ErrorType type, string message, Exception? ex = null)
    {
        var errors = new List<string> { message };
        if (ex != null)
        {
            errors.Add($"Details: {ex.Message}");
        }
        return new Error(type, errors);
    }
}