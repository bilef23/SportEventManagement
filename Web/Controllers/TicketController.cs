using System.Collections;
using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using SportEvents.Domain;

namespace Web.Controllers;

public class TicketController : Controller
{
    private readonly IEventService _eventService;
    private readonly ITicketService _ticketService;
    private readonly IShoppingCartService _shoppingCartService;
    public TicketController(IEventService eventService, ITicketService ticketService, IShoppingCartService shoppingCartService)
    {
        _eventService = eventService;
        _ticketService = ticketService;
        _shoppingCartService = shoppingCartService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
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