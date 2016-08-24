using System;
using System.Collections.Generic;

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

		public void CreateReceipt(IEnumerable<Item> items)
		{
			display.DisplayReceipt(factory.CreateReceiptFrom(items));
		}
	}
}

