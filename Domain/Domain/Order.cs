using System.Collections;
using Domain.Identity;

namespace SportEvents.Domain;

public class Order : BaseEntity
{
    public string UserId { get; set; }
    public SportEventsAppUser Owner { get; set; }
    public ICollection<Ticket> TicketsInOrder { get; set; }
}