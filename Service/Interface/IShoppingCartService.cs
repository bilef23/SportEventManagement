using Domain.DTO;
using SportEvents.Domain;

namespace Service.Interface;

public interface IShoppingCartService
{
    Task<ShoppingCart> AddTicketToCartAsync(Ticket ticket);
    ShoppingCartDTO GetShoppingCartInfo(string userId);
    Task<bool> Order(string? userId);
    Task<ShoppingCart> GetShoppingCartByOwnerId(string? userId);
    Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);
}