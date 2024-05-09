
using AutoMapper;
using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Mappings;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<CreateUpdateAccountCommand, Account>().ReverseMap();
        CreateMap<GetAccountByIdResponse, Account>().ReverseMap();
    }
}
