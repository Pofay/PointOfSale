using System;
using PointOfSale.Domain;

namespace PointOfSale.UI
{
	public class ConsoleReceiptFactory : ReceiptFactory
	{
		public ConsoleReceiptFactory()
		{
		}

		public Receipt CreateReceiptFrom(decimal price)
		{
			return new Receipt(price);
		}
	}
}

