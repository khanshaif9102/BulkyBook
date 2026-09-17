using BulkyBook.Buisness.Services;
using BulkyBook.Buisness.Services.IServices;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IApplicationUserService _applicationUserService;

        public CartController(IProductService productService, IShoppingCartService shoppingCartService, IApplicationUserService applicationUserService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _applicationUserService = applicationUserService;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cartItems = await _shoppingCartService.GetUserCartItemsAsync(userId);
            var user = await _applicationUserService.GetUserById(userId);

            ShoppingCartVM shoppingCart = new()
            {
                ShoppingCartList = cartItems,
                OrderHeader = new()
            };

            shoppingCart.OrderHeader.ApplicationUser = user;
            shoppingCart.OrderHeader.ApplicationUserId = user.Id;
            shoppingCart.OrderHeader.Name = user.Name;
            shoppingCart.OrderHeader.PhoneNumber = user.PhoneNumber;
            shoppingCart.OrderHeader.StreetAddress = user.StreetAddress;
            shoppingCart.OrderHeader.City = user.City;
            shoppingCart.OrderHeader.State = user.State;
            shoppingCart.OrderHeader.PostalCode = user.PostalCode;

            foreach (var cart in shoppingCart.ShoppingCartList)
            {
                shoppingCart.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(shoppingCart);
        }
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPOST(ShoppingCartVM shoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var cartItems = await _shoppingCartService.GetUserCartItemsAsync(userId);


            shoppingCartVM.ShoppingCartList = cartItems;

            shoppingCartVM.OrderHeader.OrderDate = DateTime.UtcNow;
            shoppingCartVM.OrderHeader.ApplicationUserId = userId;

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            //CREARE ORDER

            return View(shoppingCartVM);
        }
        public async Task<IActionResult> Plus(int cartId)
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);
            if(cart!=null)
            {
                cart.Count++;
                if (cart.Count == 100)
                {
                    //do nothing
                }
                else
                {
                    await _shoppingCartService.UpdateCartAsync(cart);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Minus(int cartId)
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);
            if(cart!=null)
            {
                cart.Count--;
                await _shoppingCartService.UpdateCartAsync(cart);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);
            if(cart!=null)
            {
                cart.Count=0;
                await _shoppingCartService.UpdateCartAsync(cart);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCart(int cartId,int count)
        {
            var cart = await _shoppingCartService.GetCartByIdAsync(cartId);
            if (cart == null) return NotFound();

            if (count <= 1)
            {
                cart.Count = 0;
                await _shoppingCartService.UpdateCartAsync(cart);
            }
            else
            {
                if (count >= 1000)
                {
                    cart.Count = 1000;
                }
                else
                {
                    cart.Count = count;
                }
            }
            
            await _shoppingCartService.UpdateCartAsync(cart);
            
            
            return Ok(new {Success = true});
        }

    }
}
