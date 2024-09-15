using Domain.Identity;
using SportEvents.Enum;

namespace SportEvents.Domain;

public class Registration : BaseEntity
{
    public DateTime RegistrationDate { get; set; }
    public RegistrationStatus Status { get; set; } 
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public Guid? ParticipantId { get; set; }
    public Participant? Participant { get; set; }
    public string UserId { get; set; }
    public SportEventsAppUser User { get; set; }
}