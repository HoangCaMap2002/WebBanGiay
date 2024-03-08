namespace WebBanGiay.ViewModels
{
	public class CartItem
	{
		public int IdProduct { get; set; }
		public string NameProduct { get; set; }
		public string ImgProduct { get; set; }
		public double PriceProduct { get; set; }
		public int QuantityProduct { get; set; }
		public string Size { get; set; }
		public double Total => PriceProduct * QuantityProduct;
	}
}
