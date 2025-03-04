using EcommerceMVC.Helpers;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{

			var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

			return View("CartPanel",new CartModel
			{
				quantity = cart.Count(cart => cart.soLuong > 0),
				total = cart.Sum(cart => cart.thanhTien)
			});
		}

	}
}
