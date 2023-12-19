using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Domain.Entities;

public class Currency:BaseAuditableEntity
{
    [Required,MaxLength(150)]
    public string CountryCurrency { get; set; }

    [Required,MaxLength(5)]
    public string Code { get; set; }

    [Required, MaxLength(5)]
    public string Symbol { get; set; }
    public Currency(string countryCurrency, 
                    string code,
                    string symbol)
    {
        CountryCurrency = countryCurrency;
        Code = code;
        Symbol = symbol;
    }

    private Currency() { }
}
