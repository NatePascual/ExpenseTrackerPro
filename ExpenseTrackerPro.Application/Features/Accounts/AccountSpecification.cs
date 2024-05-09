using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ExpenseTrackerPro.Application.Features.Accounts;

public class AccountSpecification:Specification<Account>
{

    public AccountSpecification(string searchString,TrackerFilter filter = null)
    {
        Includes.Add(a => a.AccountType);
        Includes.Add(a => a.Institution);
        Includes.Add(a => a.Currency);

        Filter = filter;
       

        if (!searchString.IsNullOrEmpty() || Filter != null)
        {
           // CreateFilter();
            //Criteria = p => (p.AccountType != null && p.Institution != null && p.Currency != null) &&
            //    (p.Name.Contains(searchString) || p.Institution.Name.Contains(searchString) ||
            //     p.AccountType.Name.Contains(searchString) || p.Currency.CountryCurrency.Contains(searchString) ||
            //     p.Currency.Code.Contains(searchString)) && p.IsHidden == false;

           Criteria = ApplyTrackerFilter(filter);
        }
        else
        {
            Criteria = p => p.AccountType != null && p.IsHidden == false;
        }
    }


    private void ModifyEntityProperty ()
    {
        //Expression<Func<Account, bool>> expression = p => p.IsHidden == false;

        if (Filter.Property == "AccountTypeName")
        {
            //ActualEntityName = "AccountType.Name";
            ActualEntityName = "AccountType.Name";
        }

        if (Filter.Property == "Name")
        {
            ActualEntityName = "Name";
        }

        if (Filter.Property == "InstitutionName")
        {
            ActualEntityName = "Institution.Name";
        }

        if (Filter.Property == "AccountNumber")
        {
            ActualEntityName = "AccountNumber";
        }

        if (Filter.Property == "CurrencyCode")
        {
            ActualEntityName = "Currency.Code";
        }
    }

    private Expression<Func<Account,bool>> ApplyTrackerFilter(TrackerFilter filter)
    {
        Expression<Func<Account, bool>> criteria = a => a.IsHidden == false;
        
     
        ModifyEntityProperty();
        if (!string.IsNullOrEmpty(ActualEntityName) &&  !string.IsNullOrEmpty(filter.FilterValue))
        {
            if(filter.FirstOperator == Specifications.Operators.Contains)
            {
                //criteria = criteria.And(a => EF.Property<string>(a, ActualEntityName) == filter.FilterValue);
                criteria = a => EF.Property<string>(a.AccountType, ActualEntityName).Contains(filter.FilterValue);
            }
            return criteria;
        }

        return criteria;
    }

}
