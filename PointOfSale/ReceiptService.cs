using System;
namespace PointOfSale.Domain
{
	public class ReceiptService
	{
		readonly ReceiptFactory factory;
		readonly ReceiptDisplay display;

		public ReceiptService(ReceiptFactory factory, ReceiptDisplay display)
		{
			this.factory = factory;
			this.display = display;
		}

		public void CreateReceipt(decimal subTotal)
		{
			display.DisplayReceipt(factory.CreateReceiptFrom(subTotal));
		}
	}
}

