using WebBanGiay.Models;

namespace WebBanGiay.ViewModels
{
	public class HomeProductViewModel
	{
		private Product product;
		private List<ProductImage> productImages;
		private List<Size> sizes;
		public Product Product { get => product; set => product = value; } 
		public List<ProductImage> ProductImages { get => productImages;set => productImages = value; }
		public List<Size> Sizes { get => sizes; set => sizes = value; }
		public HomeProductViewModel(Product product, List<ProductImage> productImages, List<Size> sizes) {
			this.product = product;
			this.productImages = productImages;
			this.sizes = sizes;
		}
	}
}
