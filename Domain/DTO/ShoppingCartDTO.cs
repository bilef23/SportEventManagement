using Domain.Identity;
using SportEvents.Domain;

namespace Domain.DTO;

public class ShoppingCartDTO
{
    public SportEventsAppUser? Owner { get; set; }
    public List<Ticket>? Tickets { get; set; }
    public double TotalPrice { get; set; }
}