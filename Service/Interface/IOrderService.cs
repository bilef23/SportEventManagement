using SportEvents.Domain;

namespace Service.Interface;

public interface IOrderService
{
    List<TicketInOrder> GetAllTicketsFromOrders(string userId);
}