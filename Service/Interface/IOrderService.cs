using SportEvents.Domain;

namespace Service.Interface;

public interface IOrderService
{
    List<Ticket> GetAllTicketsFromOrders(string userId);
}