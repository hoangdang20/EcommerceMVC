using EcommerceMVC.Data;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		public IActionResult Detail(int id)
		{
			var data = db.HangHoas.
				Include(p => p.MaLoaiNavigation)
				.SingleOrDefault(p => p.MaHh == id);

			if (data == null)
			{
				TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
				return Redirect("/404");
			}

			var result = new ChiTietHangHoaViewModel
			{
				MaHangHoa = data.MaHh,
				TenHangHoa = data.TenHh,
				Hinh = data.Hinh ?? "",
				DonGia = data.DonGia ?? 0,
				MoTaNgan = data.MoTaDonVi ?? "",
				TenLoai = data.MaLoaiNavigation.TenLoai,
				ChiTiet = data.MoTa ?? "",
				// TODO: Calculate DiemDanhGia
				DiemDanhGia = 4, 
				SoLuongTon = data.ChiTietHds.Sum(p => p.SoLuong)

			};

			return View(result);
		}
	}
}
