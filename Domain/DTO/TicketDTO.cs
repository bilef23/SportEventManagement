namespace Domain.DTO;

public class TicketDTO
{
    public int Quantity { get; set; }
    public DateTime PurchaseDate { get; set; }
    public String? Username { get; set; }
    public String? EventName { get; set; }
    public DateTime EventDate { get; set; }
    public String Location { get; set; }
}