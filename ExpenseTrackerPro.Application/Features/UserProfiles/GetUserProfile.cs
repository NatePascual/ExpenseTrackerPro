using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerPro.Application.Features.UserProfiles;

public class GetUserProfileResponse : IMapFrom<UserProfile>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public bool IsActive { get; set; }
    public string FullName { get; set; }

}

public class GetUserProfileQuery : IRequest<GetUserProfileView>
{
    public string SearchString { get; set; }

    public GetUserProfileQuery(string searchString)
    {
        SearchString = searchString;
    }
}

public class GetUserProfileView
{
    public Result<IList<GetUserProfileResponse>> UserProfiles {  get; set; }
}

internal sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileView>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public GetUserProfileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUserProfileView> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {

        var filterSpec = new UserProfileSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<UserProfile>().Entities
                     .Specify(filterSpec)
                     .ToListAsync();

        var map = _mapper.Map<IList<GetUserProfileResponse>>(getAll);

        var result = new GetUserProfileView();
        result.UserProfiles = await Result<IList<GetUserProfileResponse>>.SuccessAsync(map);

        return result;
    }
}