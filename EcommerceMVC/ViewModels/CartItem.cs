namespace EcommerceMVC.ViewModels
{
	public class CartItem
	{
		public int maHangHoa { get; set; }
		public string tenHangHoa { get; set; }
		public string hinh { get; set; }
		public double donGia { get; set; }
		public int soLuong { get; set; }
		public double thanhTien => donGia * soLuong;
	}
}
