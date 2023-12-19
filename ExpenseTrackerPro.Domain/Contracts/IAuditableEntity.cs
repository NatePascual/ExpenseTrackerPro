namespace ExpenseTrackerPro.Domain.Contracts;
public interface IAuditableEntity:IEntity
{
    string CreatedBy { get; set; }
    DateTime Created {  get; set; }
    string LastModifiedBy { get; set; }
    DateTime? LastModified { get; set; }
}
