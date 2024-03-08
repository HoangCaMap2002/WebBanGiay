using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Services
{
	public class ProductService : IProductService
	{
		private readonly QlbanGiayContext _dbcontext;
		public ProductService(QlbanGiayContext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public async Task<List<Product>> GetAllProductAsync()
		{
			List<Product> products = await _dbcontext.Products.ToListAsync();
			return products;
		}
		public async Task<List<Product>> GetProductByBrandIdAsync(int id)
		{
			return await _dbcontext.Products.Where(option => option.BrandId == id).ToListAsync();
		}

		public async Task<HomeProductViewModel?> GetProductById(int id)
		{
			try
			{
				//Lấy ra sản phầm
				var product = await _dbcontext.Products.SingleOrDefaultAsync(p => p.ProductId == id);
				if (product == null)
				{
					return null;
				}
				//Lấy size
				var sizes = product.ProductSizes.Select(ps => ps.Size).ToList();
				if (sizes ==null)
				{
					return null;
				}
				//Lấy các ảnh của sản phầm
				var imgs = await _dbcontext.ProductImages
					.Where(x => x.ProductId == id)
					.AsNoTracking()
					.ToListAsync();
				if (imgs.Count == 0)
				{
					// Nếu không có ảnh, trả về một HomeProductViewModel với danh sách ảnh trống
					return new HomeProductViewModel(product, new List<ProductImage>(), sizes);
				}
				var productDetail = new HomeProductViewModel(product, imgs, sizes);
				return productDetail;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Product>> GetProductByNameAsync(string name)
        {
			List<Product> products = await _dbcontext.Products.Where(x=>x.ProductName == name).ToListAsync();
			return products;
        }
    }
}
