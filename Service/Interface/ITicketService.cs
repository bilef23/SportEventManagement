using SportEvents.Domain;

namespace Service.Interface;

public interface ITicketService
{
    public Task<List<Ticket>> GetTickets();
    public Task<Ticket> GetTicketById(Guid? id);
    public Task<Ticket> CreateNewTicket(Ticket ticket);
    public Task<Ticket> UpdateTicket(Ticket ticket);
    public Task<Ticket> DeleteTicket(Guid id);
    MemoryStream CreatePdfTicket(Ticket ticket, int ticketNumber);
}