namespace SportEvents.Domain;

public class GameFromPartnerStore : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Platform { get; set; }
    public string Genre { get; set; }
    public float Version { get; set; }
    public float Price { get; set; } 
}