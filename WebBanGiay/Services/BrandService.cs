using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;

namespace WebBanGiay.Services
{
	public class BrandService : IBrandService
	{
		private readonly QlbanGiayContext _context;
		public BrandService(QlbanGiayContext context) { _context = context; }
		public async Task<IEnumerable<Brand>> GetAllBrandAsync()
		{
			var brands = await _context.Brands.ToListAsync();
            return brands;
		}
	}
}
