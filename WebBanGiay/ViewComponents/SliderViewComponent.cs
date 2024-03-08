using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Services;

namespace WebBanGiay.ViewComponents
{
	public class SliderViewComponent:ViewComponent
	{
		private ISliderService _sliderService;
		public SliderViewComponent(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}
		public IViewComponentResult Invoke()
		{
			var sliders = _sliderService.GetSliderAsync();
			return View(sliders);
		}
	}
}
