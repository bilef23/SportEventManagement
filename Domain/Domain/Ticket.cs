namespace SportEvents.Domain;

public class Ticket : BaseEntity
{
    public decimal Price { get; set; }
    public string SeatNumber { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string BuyerName { get; set; }
    public string BuyerEmail { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
}