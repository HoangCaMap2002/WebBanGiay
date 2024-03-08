using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBanGiay.Filters;
using WebBanGiay.Models;
using WebBanGiay.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebBanGiay.Controllers
{
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
		private readonly IProductService _productService;
		public HomeController(IProductService productService)
		{
			_productService = productService;
		}
        [AllowAnonymous]
		public async Task<IActionResult> Index()
        {
            List<Product> products = await _productService.GetAllProductAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
// dotnet ef dbcontext scaffold -o Models -d "Data Source=localhost,1433;Initial Catalog = QLBanGiay;User ID = SA;Password = Password123;TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer"
//dotnet ef dbcontext scaffold -o Models -d "Data Source=localhost,1433;Initial Catalog=QLBanGiay;User ID=SA;Password=Password123;TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer" --force
