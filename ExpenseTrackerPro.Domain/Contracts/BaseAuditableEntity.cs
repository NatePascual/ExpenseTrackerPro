
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerPro.Domain.Contracts;

public abstract class BaseAuditableEntity:IAuditableEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }

    [MaxLength(30)]
    public string CreatedBy { get; set; } = null;

    public DateTime? LastModified { get; set; } = null;

    [MaxLength(30)]
    public string LastModifiedBy { get; set; } = null;
}
