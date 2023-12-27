using AutoMapper;
using ExpenseTrackerPro.Application.Features.Incomes;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Mappings;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<CreateUpdateIncomeCommand,Income>().ReverseMap();
    }
}
