using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using WebBanGiay.Services;

namespace WebBanGiay.ViewComponents
{
	public class HangMenuViewComponent : ViewComponent
	{
		private IBrandService _brandService;
		public HangMenuViewComponent(IBrandService brandService)
		{
			_brandService = brandService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var brands = await _brandService.GetAllBrandAsync();
			return View(brands);
		}
	}
}
