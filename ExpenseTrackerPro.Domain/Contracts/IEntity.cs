using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Domain.Contracts;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}
