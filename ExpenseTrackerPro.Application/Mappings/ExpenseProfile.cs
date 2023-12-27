using AutoMapper;
using ExpenseTrackerPro.Application.Features.Expenses;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Mappings;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<CreateUpdateExpenseCommand,Expense>().ReverseMap();
    }
}
