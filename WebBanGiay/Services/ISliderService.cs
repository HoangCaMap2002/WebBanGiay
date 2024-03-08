using WebBanGiay.Models;

namespace WebBanGiay.Services
{
	public interface ISliderService
	{
		IEnumerable<Slider> GetSliderAsync();
	}
}
