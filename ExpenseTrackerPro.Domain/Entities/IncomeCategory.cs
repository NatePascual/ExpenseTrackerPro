using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class IncomeCategory : BaseAuditableEntity
{
    
    [Required, Length(5, 50)]
    public string Name { get; set; }

    [Required, MaxLength(200), Column(TypeName = "text")]
    public string ImageUrl { get; set; }

    public IncomeCategory(string name, string imageUrl)
    {
        Name = name;
        ImageUrl = imageUrl;
    }
    private IncomeCategory() { }
}
