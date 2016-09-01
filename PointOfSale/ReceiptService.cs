using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class ReceiptService : OrderFulFiller
	{
		readonly ReceiptFactory factory;
		readonly ReceiptDisplay display;
		readonly TransactionIdGenerator generator;


		public ReceiptService(ReceiptFactory factory, ReceiptDisplay display, TransactionIdGenerator generator)
		{
			this.factory = factory;
			this.generator = generator;
			this.display = display;
		}

		public void FulFillOrder(IEnumerable<Item> items)
		{
			int id = generator.GenerateTransactionId();
			display.DisplayReceipt(factory.CreateReceiptFrom(id, items));
		}
	}
}

