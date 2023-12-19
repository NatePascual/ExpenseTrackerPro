using ExpenseTrackerPro.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerPro.Domain.Entities;

public class Category : BaseAuditableEntity
{

    [ForeignKey(nameof(Parent.Id))]
    public int? ParentId { get; set; } = null;
    public Category Parent { get; set; } = null;

    [Required, Length(5, 50)]
    public string Name { get; set; }

    [Required, MaxLength(200), Column(TypeName = "text")]
    public string ImageUrl { get; set; }

    public ICollection<Category> ChildCategories { get; set; } = new HashSet<Category>();

    public Category(int? parentId, 
                    string name,
                    string  imageUrl)
    {
        ParentId = parentId;
        Name = name;
        ImageUrl = imageUrl;
    }

    private Category() { }
}
