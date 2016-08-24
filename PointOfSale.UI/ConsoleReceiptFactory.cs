using System;
using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class ConsoleReceiptFactory : ReceiptFactory
	{
		public ConsoleReceiptFactory()
		{
		}

		public Receipt CreateReceiptFrom(IEnumerable<Item> items)
		{
			return new Receipt(items);
		}


	}
}

