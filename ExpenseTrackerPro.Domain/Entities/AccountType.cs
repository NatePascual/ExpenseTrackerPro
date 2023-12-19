
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class AccountType : BaseAuditableEntity
{
    [Required, Length(20, 50)]
    public string Name { get; set; }

    [Required, MaxLength(30)]
    public string Classification { get; set; }

    [Required, MaxLength(200), Column(TypeName = "text")]
    public string ImageUrl { get; set; }

    public AccountType(string name, 
                       string classification,
                       string imageUrl)
    {
        Name = name;
        Classification = classification;
        ImageUrl = imageUrl;
    }

    private AccountType() { }
}
