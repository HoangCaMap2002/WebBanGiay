using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Main Image")]
        public List<Dictionary<string, string>> Size { get; set; }
        public int BrandId { get; set; }
        public IFormFile MainImage { get; set; }

        [Display(Name = "Images")]
        public List<IFormFile> Images { get; set; }
    }
}
