using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Stripe;

namespace Web.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    // GET
    [Authorize]
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dto = _shoppingCartService.GetShoppingCartInfo(userId);
        return View(dto);
    }
    public IActionResult Order()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = _shoppingCartService.Order(userId);            
        //if (result == true)
        return RedirectToAction("Index", "ShoppingCarts");


    }

    public IActionResult SuccessPayment()
    {
        return View();
    }

    public async Task<IActionResult> DeleteFromShoppingCart(Guid? id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // Get the shopping cart of the current user
        var shoppingCart = await _shoppingCartService.GetShoppingCartByOwnerId(userId);
    
        if (shoppingCart == null)
        {
            return NotFound();
        }

        // Find the ticket in the shopping cart by ticketId
        var ticket = shoppingCart.Tickets?.FirstOrDefault(t => t.Id == id);

        if (ticket != null)
        {
            // Remove the ticket from the shopping cart
            shoppingCart.Tickets.Remove(ticket);
        
            // Update the shopping cart in the database
            await _shoppingCartService.UpdateShoppingCart(shoppingCart);
        }

        // Redirect back to the shopping cart view (or another appropriate view)
        return RedirectToAction("Index");
    }
    
    public IActionResult PayOrder(string stripeEmail, string stripeToken)
    {
        StripeConfiguration.ApiKey = "sk_test_51Pw22BRsDJDll71n2HNfCoeTBkiAG5HQC27XxVkfBdVV4Gp4PkY9dV3LzI37Wk6nncgViytsZxwjglWqqAKNu7vb00CqN6dz3S";
        var customerService = new CustomerService();
        var chargeService = new ChargeService();
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var order = this._shoppingCartService.GetShoppingCartInfo(userId);

        var customer = customerService.Create(new CustomerCreateOptions
        {
            Email = stripeEmail,
            Source = stripeToken
        });

        var charge = chargeService.Create(new ChargeCreateOptions
        {
            Amount = (Convert.ToInt32(order.TotalPrice) * 100),
            Description = "Sport Events Management Application Payment",
            Currency = "usd",
            Customer = customer.Id
        });

        if (charge.Status == "succeeded")
        {
            this.Order();
            return RedirectToAction("SuccessPayment");

        }
        else
        {
            return RedirectToAction("NotSuccessPayment");
        }
    }

    public IActionResult NotSuccessPayment()
    {
        return View();
    }
}