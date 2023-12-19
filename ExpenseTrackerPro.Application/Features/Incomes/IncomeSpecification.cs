using ExpenseTrackerPro.Application.Specifications;
using ExpenseTrackerPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerPro.Application.Features.Incomes;

public class IncomeSpecification : Specification<Income>
{
    
    public IncomeSpecification(string searchString)
    {
        Includes.Add(a => a.IncomeCategory);
        Includes.Add(a => a.Account);
        
        if(!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.IncomeCategory != null && p.Account != null) && (p.Note.Contains(searchString) || p.IncomeCategory.Name.Contains(searchString) || p.Account.Name.Contains(searchString));
        }
        else
        {
            Criteria = p => (p.IncomeCategory != null && p.Account != null);
        }
    }
}
