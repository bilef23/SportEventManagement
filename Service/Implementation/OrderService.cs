using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IRepository<Order> orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public List<TicketInOrder> GetAllTicketsFromOrders(string userId)
    {
        var user = _userRepository.Get(userId);
        var allOrders= user.Orders.SelectMany(o => o.TicketsInOrder).ToList();

        return allOrders;
    }
}