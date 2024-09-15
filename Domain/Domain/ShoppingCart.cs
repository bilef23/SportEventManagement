using Domain.Identity;

namespace SportEvents.Domain;

public class ShoppingCart : BaseEntity
{
    public string? OwnerId { get; set; }
    public SportEventsAppUser? Owner { get; set; }
    public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();

}