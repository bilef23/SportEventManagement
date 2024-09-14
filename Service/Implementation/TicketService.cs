using Domain.DTO;
using GemBox.Document;
using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;


namespace Service.Implementation;

public class TicketService : ITicketService
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(IRepository<Ticket> context, IUnitOfWork unitOfWork)
    {
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        _ticketRepository = context;
        _unitOfWork = unitOfWork;
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
        ticket.PurchaseDate=DateTime.UtcNow;
        await _ticketRepository.Insert(ticket);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return ticket;
    }

    public async Task<Ticket> UpdateTicket(Ticket ticket)
    {
        await _ticketRepository.Update(ticket);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return ticket;
    }

    public MemoryStream CreatePdfTicket(Ticket ticket, int ticketNumber)
    {
        Console.WriteLine("Ok");
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Ticket-Template.docx");
        var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), $"Ticket-{ticketNumber}.pdf");

        var document = DocumentModel.Load(templatePath);
        
        document.Content.Replace("{{Event}}", ticket.Event.Name);
        document.Content.Replace("{{Date}}", ticket.Event.StartDate.Date.ToShortDateString());
        document.Content.Replace("{{Time}}", ticket.Event.StartDate.TimeOfDay.ToString());
        document.Content.Replace("{{Location}}", ticket.Event.Location);
        document.Content.Replace("{{Price}}", ticket.Event.EventPrice.ToString());
            
        var pdfStream = new MemoryStream();
        document.Save(pdfStream, SaveOptions.PdfDefault);
    
        pdfStream.Position = 0; // Reset stream position to the beginning
        return pdfStream;
    }

    public async Task<Ticket> DeleteTicket(Guid id)
    {
        var ticket =await  GetTicketById(id);
        
        await _ticketRepository.Delete(ticket);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return ticket;
    }
}