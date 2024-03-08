using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;

namespace WebBanGiay.Services
{
	public class SliderService : ISliderService
	{
		private readonly QlbanGiayContext _context;
		public SliderService(QlbanGiayContext context)
		{
			_context = context;
		}
		public IEnumerable<Slider> GetSliderAsync()
		{
			return _context.Sliders;
		}
	}
}
