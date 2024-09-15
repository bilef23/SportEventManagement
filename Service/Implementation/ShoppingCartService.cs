using Domain;
using Domain.DTO;
using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IRepository<ShoppingCart> _shoppingCartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly ITicketService _ticketService;
    private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, ITicketService ticketService, IRepository<TicketInOrder> ticketInOrderRepository, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _ticketService = ticketService;
        _ticketInOrderRepository = ticketInOrderRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<ShoppingCart> AddTicketToCartAsync(Ticket ticket)
    {
        var loggedInUser = _userRepository.Get(ticket.UserId);
        var userShoppingCart = loggedInUser.ShoppingCart;
        
        if (userShoppingCart?.Tickets == null)
            userShoppingCart.Tickets = new List<Ticket>(); ;

        userShoppingCart.Tickets.Add(ticket);
        await _shoppingCartRepository.Update(userShoppingCart);
        await _unitOfWork.SaveChangesAsync();
        
        return userShoppingCart;
    }
    
    public ShoppingCartDTO GetShoppingCartInfo(string userId)
    {
        var loggedInUser = _userRepository.Get(userId);

        var userShoppingCart = loggedInUser?.ShoppingCart;
        var allTickets = userShoppingCart?.Tickets?.ToList();

        var totalPrice = allTickets.Select(t => (t.Event.EventPrice * t.Quantity)).Sum();

        ShoppingCartDTO dto = new ShoppingCartDTO()
        {
            Tickets = allTickets,
            TotalPrice = totalPrice
        };
        return dto;
    }

    public async Task<bool> Order(string? userId)
    {
        if (userId != null)
        {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;
            
                EmailMessage message = new EmailMessage();
                message.Subject = "Sport event Tickets";
                message.MailTo = loggedInUser.Email;
                message.Content = "Successfull order";
                var tickets = userShoppingCart.Tickets;
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                };
                 await _orderRepository.Insert(order);
                 List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();
                 var list = userShoppingCart.Tickets.Select(
                     x => new TicketInOrder()
                     {
                         Id = Guid.NewGuid(),
                         TicketId = x.Id,
                         OrderId = order.Id,
                         Quantity = x.Quantity
                     }
                 ).ToList();
                 ticketInOrders.AddRange(list);
                 
                 foreach (var ticket in ticketInOrders)
                 {
                     await _ticketInOrderRepository.Insert(ticket);
                 }

                 List<MemoryStream> attachments = new List<MemoryStream>();
                foreach (var ticket in tickets)
                {
                   
                    for (int i = 0; i < ticket.Quantity; i++)
                    {
                        attachments.Add(_ticketService.CreatePdfTicket(ticket, i));
                    }
                    
                }

                loggedInUser.ShoppingCart.Tickets.Clear();
                 _userRepository.Update(loggedInUser);

                 await _unitOfWork.SaveChangesAsync();
                await _emailService.SendEmailAsync(message, attachments);

                return true;
        }
        return false;
    }

    public async Task<ShoppingCart> GetShoppingCartByOwnerId(string? userId)
    {
        var shoppingCarts = await _shoppingCartRepository.GetAll(e=>e.Tickets);
        var result = shoppingCarts.Find(sc => sc.OwnerId.Equals(userId));

        return result;
    }

    public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
    {
        await _shoppingCartRepository.Update(shoppingCart);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return shoppingCart;
    }
}