using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.UserProfiles;

public class UserProfileSpecification  : Specification<UserProfile>
{
    public UserProfileSpecification(string searchString)
    {
        if(!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.FullName.Contains(searchString) || p.Email.Contains(searchString) || p.Mobile.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }

}
