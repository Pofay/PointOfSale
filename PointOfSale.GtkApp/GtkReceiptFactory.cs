using System;
using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.GtkApp
{
	public class GtkReceiptFactory : ReceiptFactory
	{
		public GtkReceiptFactory()
		{
		}

		public Receipt CreateReceiptFrom(int transactionId, IEnumerable<Item> items)
		{
			return new Receipt(transactionId, items);
		}
	}
}

