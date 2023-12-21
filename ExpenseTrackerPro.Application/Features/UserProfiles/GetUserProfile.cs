using AutoMapper;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Common.Mappings;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Wrappers;
using MediatR;
using System.Linq.Expressions;

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
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetUserProfileQuery(int pageNumber, int pageSize, string searchString)
    {
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

public class GetUserProfileView
{
    public Result<PaginatedResult<GetUserProfileResponse>> UserProfiles {  get; set; }
}

internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileView>
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
        Expression<Func<UserProfile, GetUserProfileResponse>> expression = e => new GetUserProfileResponse()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            FullName = e.FullName,
            Email = e.Email,
            Mobile = e.Mobile,
            IsActive = e.IsActive
        };

        var filterSpec = new UserProfileSpecification(request.SearchString);

        var getAll = await _unitOfWork.Repository<UserProfile>().Entities
                     .Specify(filterSpec)
                     .Select(expression)
                     .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        var map = _mapper.Map<PaginatedResult<GetUserProfileResponse>>(getAll);

        var result = new GetUserProfileView();
        result.UserProfiles = await Result<PaginatedResult<GetUserProfileResponse>>.SuccessAsync(map);

        return result;
    }
}