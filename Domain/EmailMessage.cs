namespace Domain;

public class EmailMessage
{
    public string? MailTo { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }
    public Boolean Status { get; set; }
}