using WebBanGiay.Models;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Services
{
	public interface IProductService
	{
		Task<List<Product>> GetAllProductAsync();
		Task<List<Product>> GetProductByBrandIdAsync(int id);
		Task<List<Product>> GetProductByNameAsync(string name);
		Task<HomeProductViewModel?> GetProductById(int id);
		
	}
}
