using EcommerceMVC.Data;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Helpers;

namespace EcommerceMVC.Controllers
{
    public class CartController : Controller
    {
		private readonly Hshop2023Context db;

		public CartController(Hshop2023Context context)
        {
            db = context;
        }

		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

		public IActionResult Index()
        {
            return View(Cart);
        }

		public IActionResult AddToCart(int id, int quantity = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.maHangHoa == id);

			if (item == null) {
				var product = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if(product == null)
				{
					TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
					return Redirect("/404");
				}
					item = new CartItem
					{
						maHangHoa = product.MaHh,
						tenHangHoa = product.TenHh,
						hinh = product.Hinh ?? string.Empty,
						donGia = product.DonGia ?? 0,
						soLuong = quantity
					};
					gioHang.Add(item);
			}
			else
			{
				item.soLuong += quantity;
			}

			HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.maHangHoa == id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
	}
}
