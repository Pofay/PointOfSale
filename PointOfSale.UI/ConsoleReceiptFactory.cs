using System;
using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSale.ConsoleApp
{
	public class ConsoleReceiptFactory : ReceiptFactory
	{
		public ConsoleReceiptFactory()
		{
		}

		public Receipt CreateReceiptFrom(int transactionId, IEnumerable<Item> items)
		{
			return new Receipt(transactionId, items);
		}


	}
}

