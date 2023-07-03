namespace ToDoList.Application.Exceptions;

public class AcessDeniedException : ApplicationException
{
    public AcessDeniedException(string msg) : base(msg)
    {

    }
}
