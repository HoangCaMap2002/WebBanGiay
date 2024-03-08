namespace WebBanGiay.ViewModels
{
	public class CheckOutViewModel
	{
		public List<CartItem>? CartItems { get; set; }
		public int OrderId { get; set; }

		public DateTime? OrderDate { get; set; }

		public string ShippingAddress { get; set; } = null!;

		public string? Email { get; set; }

		public string? Phone { get; set; }

		public double TotalAmount { get; set; }

		public int? PaymentMethodId { get; set; }

		public string? ShippingStatus { get; set; }

		public int? UserId { get; set; }

	}
}
