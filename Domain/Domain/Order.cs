using System.Collections;
using Domain.Identity;

namespace SportEvents.Domain;

public class Order : BaseEntity
{
    public string OwnerId { get; set; }
    public SportEventsAppUser Owner { get; set; }
    public ICollection<TicketInOrder> TicketsInOrder { get; set; } = new List<TicketInOrder>();
}