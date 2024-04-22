namespace TasksProcessing.Logic.Exceptions;

public class LogicException : Exception
{
    public LogicException() { }

    public LogicException(string message) : base(message) { }

    public LogicException(string message, Exception inner) : base(message, inner) { }
}
