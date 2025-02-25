using EcommerceMVC.Data;
using EcommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;


         //invoke dùng để trả về view component
        public IViewComponentResult Invoke()
        {

            var items = db.Loais.Select(loai => new MenuLoaiViewModel
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
                SoLuong = loai.HangHoas.Count
            }).OrderBy(cmp => cmp.TenLoai).ToList();
            // Default.cshtml 
            return View("Default", items);
            //hoặc 
            //return View(items);
        }
    }
}
