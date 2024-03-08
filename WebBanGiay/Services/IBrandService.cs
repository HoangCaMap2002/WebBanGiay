using WebBanGiay.Models;

namespace WebBanGiay.Services
{
	public interface IBrandService
	{
		Task<IEnumerable<Brand>> GetAllBrandAsync();
	}
}
