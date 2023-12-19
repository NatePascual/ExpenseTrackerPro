using System.Globalization;

namespace ExpenseTrackerPro.Application.Common.Exceptions;

public class ServiceException : Exception
{
    public ServiceException() : base()
    {
    }

    public ServiceException(string message) : base(message)
    {
    }

    public ServiceException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
