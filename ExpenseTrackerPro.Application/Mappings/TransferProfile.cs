using AutoMapper;
using ExpenseTrackerPro.Application.Features.Transfers;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Mappings;

public class TransferProfile : Profile
{
    public TransferProfile()
    {
        CreateMap<CreateUpdateTransferCommand,Transfer>().ReverseMap();
    }
}
