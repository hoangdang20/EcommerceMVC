using EcommerceMVC.Data;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly Hshop2023Context db;

		public HangHoaController(Hshop2023Context context)
		{
			db = context;
		}
		public IActionResult Index(int? loai)
		{
			var hangHoas = db.HangHoas.AsQueryable();

			if (loai.HasValue)
			{
				hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
			}

			var result = hangHoas.Select(p => new HangHoaViewModel
			{
				MaHangHoa = p.MaHh,
				TenHangHoa = p.TenHh,
				Hinh = p.Hinh ?? "",
				DonGia = p.DonGia ?? 0,
				MoTaNgan = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai
				});

			return View(result);
		}

		public IActionResult Search(string? query)
		{
			var hangHoas = db.HangHoas.AsQueryable();

			if (query != null)
			{
				hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
			}

			var result = hangHoas.Select(p => new HangHoaViewModel
			{
				MaHangHoa = p.MaHh,
				TenHangHoa = p.TenHh,
				Hinh = p.Hinh ?? "",
				DonGia = p.DonGia ?? 0,
				MoTaNgan = p.MoTaDonVi ?? "",
				TenLoai = p.MaLoaiNavigation.TenLoai
			});

			return View(result);
		}
	}
}
