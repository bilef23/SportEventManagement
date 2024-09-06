using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class TicketService : ITicketService
{
    private readonly IRepository<Ticket> _ticketRepository;

    public TicketService(IRepository<Ticket> context)
    {
        _ticketRepository = context;
    }

    public async Task<List<Ticket>> GetTickets()
    {
        return await _ticketRepository.GetAll();
    }

    public async Task<Ticket> GetTicketById(Guid? id)
    {
        var result = await _ticketRepository.Get(id);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Ticket> CreateNewTicket(Ticket ticket)
    {
        var result = await _ticketRepository.Insert(ticket);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Ticket> UpdateTicket(Ticket ticket)
    {
        var result = await _ticketRepository.Update(ticket);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Ticket> DeleteTicket(Guid id)
    {
        var ticket =await  GetTicketById(id);
        
        var result = await _ticketRepository.Delete(ticket);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
}