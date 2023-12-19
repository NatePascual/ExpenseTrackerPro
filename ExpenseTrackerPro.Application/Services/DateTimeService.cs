using ExpenseTrackerPro.Application.Common.Interfaces;

namespace ExpenseTrackerPro.Application.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;

}