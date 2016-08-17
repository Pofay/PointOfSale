namespace PointOfSale.Domain
{
	public interface Display
	{
		void DisplayScannedItem(Item item);

		void DisplayReceipt(Receipt receipt);
	}
}