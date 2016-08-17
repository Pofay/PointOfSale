namespace PointOfSale.Domain
{
	public class Receipt
	{
		private readonly decimal price;

		public Receipt(decimal totalPrice)
		{
			this.price = totalPrice;
		}

		public override string ToString()
		{
			return string.Format("Total Price:D {0}", price);
		}
	}
}