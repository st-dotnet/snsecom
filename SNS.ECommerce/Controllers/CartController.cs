using Microsoft.AspNetCore.Mvc;
using SNS.ECommerce.Infrastructure.Interfaces;
using System.Linq;

namespace SNS.ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICartService _cartService;
        public CartController(IProductServices productServices, ICartService cartService)
        {
            _productServices = productServices;
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            var cartProducts = _cartService.GetCartItems().ToList();
            ViewBag.CartList = cartProducts;
            return View();
        }
        /// <summary>
        /// AddItemToShoppingCart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Cart/AddItemToShoppingCart/{id}")]
        public IActionResult AddItemToShoppingCart(int Id)
        {
            var item = _productServices.GetProductById(Id);
            if (item != null)
            {
                _cartService.AddItemToCart(item);
            }
            return RedirectToAction("Index", "Cart");
        }
        /// <summary>
        /// DeleteCartItem
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public IActionResult DeleteCartItem(int Id)
        {
            var data = _cartService.RemoveFromCart(Id);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// DeleteCart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/Cart/DeleteCart/{id}")]
        public IActionResult DeleteFromCart(int Id)
        {
            var data = _cartService.RemoveCart(Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("/Cart/DeleteCart/{id}")]
        public IActionResult DeleteCart(int Id)
        {
            var data = _cartService.RemoveCart(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}