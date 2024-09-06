using SportEvents.Enum;

namespace SportEvents.Domain;

public class Event : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EventType EventType { get; set; }
    public Guid OrganizerId { get; set; }
    public Organizer? Organizer { get; set; }
    public ICollection<Registration>? Registrations { get; set; }
}
