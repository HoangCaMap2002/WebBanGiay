using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.Services;
using WebBanGiay.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebBanGiay.Areas.Areas.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : Controller
	{
		private readonly IProductService _productService;
		QlbanGiayContext db = new QlbanGiayContext();
		public HomeAdminController(IProductService productService)
		{
			_productService= productService;
		}
        public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> QLProduct()
		{
			var products = await _productService.GetAllProductAsync();
			return View(products);
		}
		[HttpGet]
		public async Task<IActionResult> AddProduct()
		{
            ViewBag.BrandId = new SelectList(await db.Brands.ToListAsync(), "BrandId", "BrandName");
            return View();
		}
		[HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
		{
            ProductViewModel p = model;
           if (ModelState.IsValid)
           {
               // Xử lý logic lưu trữ ảnh đại diện chính
               string mainImageFileName = Guid.NewGuid().ToString() + "_" + model.MainImage.FileName;
               string mainImagePath = Path.Combine("wwwroot/assets/imageproduct", mainImageFileName);
          
               using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
               {
                   await model.MainImage.CopyToAsync(mainImageStream);
               }
          
               // Xử lý logic lưu trữ ảnh phụ
               List<string> additionalImagePaths = new List<string>();
               foreach (var additionalImage in model.Images)
               {
                   string additionalImageFileName = Guid.NewGuid().ToString() + "_" + additionalImage.FileName;
                   string additionalImagePath = Path.Combine("wwwroot/assets/imageproduct", additionalImageFileName);
          
                   using (var additionalImageStream = new FileStream(additionalImagePath, FileMode.Create))
                   {
                       await additionalImage.CopyToAsync(additionalImageStream);
                   }
          
                   additionalImagePaths.Add(additionalImageFileName);
               }
          
                //thêm ở bảng product
               var product = new Product
               {
                   ProductName = model.ProductName,
                   Price = model.Price,
                   Description = model.Description,
                   Quantity = model.Quantity,
                   BrandId = model.BrandId,
                   Img = mainImageFileName,
               };
               db.Products.Add(product);
               await db.SaveChangesAsync();
               //Thêm ở bảng ProductImage
               foreach (var i in additionalImagePaths)
               {
                   var productimg = new ProductImage
                   {
                       ProductId = product.ProductId,
                       ImageUrl = i,
                   };
                   db.ProductImages.Add(productimg);
                   await db.SaveChangesAsync();
               }
                //Thêm size
                foreach (var item in model.Size)
                {
                    var sizeValue = item["Size"];
                    if (int.TryParse(item["Quantity"], out int quantity))
                    {
                        var existsize = await db.Sizes.FirstOrDefaultAsync(s => s.SizeValue == sizeValue);
                        if (existsize != null)
                        {
                            //Thêm vào bảng ProductSize
                            var productsize = new ProductSize
                            {
                                ProductId = product.ProductId,
                                SizeId = existsize.SizeId,
                                Quanity = quantity
                            };
                            db.ProductSizes.Add(productsize);
                            await db.SaveChangesAsync();
                        }
                        else
                        {
                            //Thêm vào bảng Size
                            var size = new Size
                            {
                                SizeValue = sizeValue,
                            };
                            db.Sizes.Add(size);
                            //await db.SaveChangesAsync();
                            //Thêm vào bảng 
                            var productsize = new ProductSize
                            {
                                ProductId = product.ProductId,
                                SizeId = size.SizeId,
                                Quanity = quantity
                            };
                            db.ProductSizes.Add(productsize);
                            await db.SaveChangesAsync();
                        }
                    }
                    else
                    {

                    }
                }
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm sản phẩm thành công
           }
          
           // Nếu ModelState không hợp lệ, quay lại form để hiển thị thông báo lỗi
          return View(model);
        }
    }
}
