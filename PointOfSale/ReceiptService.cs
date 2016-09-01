using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class ReceiptService : OrderFulFiller
	{
		readonly ReceiptFactory factory;
		readonly ReceiptDisplay display;

		public ReceiptService(ReceiptFactory factory, ReceiptDisplay display)
		{
			this.factory = factory;
			this.display = display;
		}

		public void FulFillOrder(int transactionId, IEnumerable<Item> items)
		{
			display.DisplayReceipt(factory.CreateReceiptFrom(transactionId, items));
		}
	}
}

