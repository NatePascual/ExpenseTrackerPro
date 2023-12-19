using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;

namespace ExpenseTrackerPro.Application.Features.Currencies;

public class CurrencySpecification : Specification<Currency>
{
    public CurrencySpecification(string searchString)
    {
        if(!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.CountryCurrency.Contains(searchString) || p.Code.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }
}
