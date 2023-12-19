using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ExpenseTrackerPro.Domain.Contracts;

namespace ExpenseTrackerPro.Domain.Entities;

public class UserProfile : BaseAuditableEntity
{

    [Length(15, 30), Required]
    public string FirstName { get; set; }

    [Length(15, 30), Required]
    public string LastName { get; set; }

    [Length(50, 100), Required, EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Length(11, 20)]
    public string Mobile { get; set; }

    public bool IsActive { get; set; }

    [MaxLength(200)]
    public string ImageUrl { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return FirstName + ' ' + LastName;
        }
    }

    public UserProfile(string firstName,
                       string lastName,
                       string email,
                       string mobile,
                       string imageUrl,
                       bool isActive)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Mobile = mobile;
        ImageUrl = imageUrl;
        IsActive = isActive;
    }

    private UserProfile() { }
}