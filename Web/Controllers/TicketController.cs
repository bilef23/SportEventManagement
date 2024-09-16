using System.Collections;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Service.Interface;
using SportEvents.Domain;

namespace Web.Controllers;

public class TicketController : Controller
{
    private readonly IEventService _eventService;
    private readonly ITicketService _ticketService;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IOrderService _orderService;
    public TicketController(IEventService eventService, ITicketService ticketService, IShoppingCartService shoppingCartService, IOrderService orderService)
    {
        _eventService = eventService;
        _ticketService = ticketService;
        _shoppingCartService = shoppingCartService;
        _orderService = orderService;
    }

    // GET
    [Authorize]
    public IActionResult Index()
    {
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var allTickets = _orderService.GetAllTicketsFromOrders(userId);
        
        var allTicketsDto = allTickets.Select(ticket => new TicketDTO
        {
            Quantity = ticket.Quantity,
            PurchaseDate = ticket.Ticket.PurchaseDate,
            Username = ticket.Ticket.User.FirstName+"'s", // Assuming `User` is a property in `Ticket`
            EventName = ticket.Ticket.Event.Name,
            EventDate = ticket.Ticket.Event.StartDate,
            Location = ticket.Ticket.Event.Location// Assuming `Event` is a property in `Ticket`
        }).ToList();
        
        return View(allTicketsDto);
    }

    [Authorize]
    public async Task<IActionResult> ExportTicketsToExcel()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var allTickets = _orderService.GetAllTicketsFromOrders(userId);
        
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Tickets");

            // Add headers
            worksheet.Cells[1, 1].Value = "Event";
            worksheet.Cells[1, 2].Value = "Date";
            worksheet.Cells[1, 3].Value = "Location";
            worksheet.Cells[1, 4].Value = "Quantity";
            worksheet.Cells[1, 5].Value = "Purchase Date";

            // Add data rows
            for (int i = 0; i < allTickets.Count; i++)
            {
                var ticket = allTickets[i];
                worksheet.Cells[i + 2, 1].Value = ticket.Ticket.Event.Name; // Event Name
                worksheet.Cells[i + 2, 2].Value = ticket.Ticket.Event.StartDate.ToString("yyyy-MM-dd"); // Event Date
                worksheet.Cells[i + 2, 3].Value = ticket.Ticket.Event.Location; // Event Location
                worksheet.Cells[i + 2, 4].Value = ticket.Quantity; // Ticket Quantity
                worksheet.Cells[i + 2, 5].Value = ticket.Ticket.PurchaseDate.ToString("yyyy-MM-dd HH:mm:ss"); // Purchase Date
            }

            // Auto-fit columns for all cells
            worksheet.Cells.AutoFitColumns();

            // Prepare the response with the Excel file
            var excelData = package.GetAsByteArray();
            var fileName = "Tickets.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }

    public async Task<IActionResult> BuyTicket(Guid id)
    {
        Event result = await _eventService.GetEventById(id);
        var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        Ticket ticket = new Ticket();
        ticket.UserId = userId;
        ticket.EventId = result.Id;
        ticket.Event = result;
        
        return View(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> AddToShoppingCart(Ticket ticket)
    {
        Ticket adddedTicket = await _ticketService.CreateNewTicket(ticket);
        ShoppingCart cart = await _shoppingCartService.AddTicketToCartAsync(adddedTicket);
        
        return RedirectToAction("Index","ShoppingCart",cart);
    }
}