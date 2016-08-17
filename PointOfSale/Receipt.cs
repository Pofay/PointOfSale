namespace PointOfSale.Domain
{
	public class Receipt
	{
		private readonly decimal price;

		public Receipt(decimal totalPrice)
		{
			this.price = totalPrice;
		}
	}
}