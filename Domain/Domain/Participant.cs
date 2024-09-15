using SportEvents.Enum;

namespace SportEvents.Domain;

public class Participant : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; } 
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Registration>? Registrations { get; set; } = new List<Registration>();
}