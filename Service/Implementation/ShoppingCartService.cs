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

    public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ShoppingCart> AddTicketToCartAsync(Ticket ticket)
    {
        var loggedInUser = _userRepository.Get(ticket.UserId);
        var userShoppingCart = loggedInUser.ShoppingCart;
        
        if (userShoppingCart.Tickets == null)
            userShoppingCart.Tickets = new List<Ticket>(); ;

        userShoppingCart.Tickets.Add(ticket);
        await _shoppingCartRepository.Update(userShoppingCart);
        
        return userShoppingCart;
    }
    
    public ShoppingCartDTO getShoppingCartInfo(string userId)
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
                //EmailMessage message = new EmailMessage();
                //message.Subject = "Successfull order";
                //message.MailTo = loggedInUser.Email;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TicketsInOrder = userShoppingCart.Tickets
                };
                var result =await _orderRepository.Insert(order);
                
                loggedInUser.Orders.Add(order);
                _userRepository.Update(loggedInUser);
                
                 
                
                //List<ProductInOrder> productInOrder = new List<ProductInOrder>();

                


                //StringBuilder sb = new StringBuilder();

                //var totalPrice = 0.0;

                //sb.AppendLine("Your order is completed. The order conatins: ");

                /*for (int i = 1; i <= lista.Count(); i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Product.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Product.ProductName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Product.Price);
                }*/

                /*sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _productInOrderRepository.Insert(product);
                }*/

                loggedInUser.ShoppingCart.Tickets.Clear();
                _userRepository.Update(loggedInUser);
                //this._emailService.SendEmailAsync(message);

                return true;
            }
            return false;
    }
}