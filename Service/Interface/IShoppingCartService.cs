using Domain.DTO;
using SportEvents.Domain;

namespace Service.Interface;

public interface IShoppingCartService
{
    Task<ShoppingCart> AddTicketToCartAsync(Ticket ticket);
    ShoppingCartDTO getShoppingCartInfo(string userId);
    bool Order(string? userId);
}