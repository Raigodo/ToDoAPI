namespace ToDoList.Application.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string msg) : base(msg)
    {

    }
}