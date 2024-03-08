using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Helper;
using WebBanGiay.Models;
using WebBanGiay.Services;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		QlbanGiayContext db = new QlbanGiayContext();
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<IActionResult> ProductByBrandId(int id)
		{
			List<Product> products = await _productService.GetProductByBrandIdAsync(id);
			return View(products);
		}
		public async Task<IActionResult> SearchProduct(string searchInput)
		{
			List<Product> products = await _productService.GetProductByNameAsync(searchInput);
			return View(products);
		}
		[AllowAnonymous]
		public async Task<IActionResult> ProductDetail(int id)
		{
			HomeProductViewModel? product = await _productService.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		//Cart
		public List<CartItem> Carts
		{
			get
			{
				var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
				if (data == null)
				{
					data = new List<CartItem>();
				}
				return data;
			}
		}
		[AllowAnonymous]
		public async Task<IActionResult> IndexCart()
		{
			return View(Carts);
		}
		[AllowAnonymous]
		public async Task<IActionResult> AddToCart(int id, int numberproduct, string selectedSize)
		{
			var myCart = Carts;
			var item = myCart.SingleOrDefault(p => p.IdProduct == id);
			if (item == null)
			{
				var product = await _productService.GetProductById(id);
				item = new CartItem
				{
					IdProduct = id,
					NameProduct = product.Product.ProductName,
					ImgProduct = product.Product.Img,
					PriceProduct = product.Product.Price,
					QuantityProduct = numberproduct,
					Size= selectedSize
				};
				myCart.Add(item);
			}
			else
			{
				item.QuantityProduct += numberproduct;

			}
			HttpContext.Session.Set("GioHang", myCart);
			return RedirectToAction("IndexCart");
		}
		[HttpGet]

		public async Task<IActionResult> CheckOut()
		{
			var myCart = Carts;
			ViewBag.PaymentMethodId = new SelectList(await db.PaymentMethods.ToListAsync(), "PaymentMethodId", "MethodName");
			CheckOutViewModel n = new CheckOutViewModel();

			n.CartItems = myCart;
			return View(n);
		}
		[HttpPost]
		public async Task<IActionResult> CheckOut(CheckOutViewModel model)
		{
			var myCart = Carts;
			var userid = HttpContext.Session.GetInt32("UserId");
			var totalamount = myCart.Sum(x => x.Total);
			if (userid !=null)
			{
				//thêm bảng order
				var order = new Order
				{
					UserId = userid,
					OrderDate = DateTime.Now,
					ShippingAddress = model.ShippingAddress,
					Email = model.Email,
					Phone = model.Phone,
					PaymentMethodId = model.PaymentMethodId,
					TotalAmount = totalamount,
					ShippingStatus = "Đang xử lý"
				};
				db.Orders.Add(order);
				await db.SaveChangesAsync();
				//thêm orderdatail
				foreach (var item in myCart)
				{
					
					var orderdetail = new OrderDetail
					{
						OrderId = order.OrderId,
						ProductId = item.IdProduct,
						Quantity = item.QuantityProduct,
						PricePerUnit = item.Total,
						SizeName = item.Size
					};
					db.OrderDetails.Add(orderdetail);
					await db.SaveChangesAsync();
				}
				return Redirect("/Product/OrderSuccess");
			}
			return View();
		}
		public async Task<IActionResult> OrderSuccess()
		{
			return View();
		}
	}
}
