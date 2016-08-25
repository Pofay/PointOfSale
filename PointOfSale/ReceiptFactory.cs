using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface ReceiptFactory
	{
		Receipt CreateReceiptFrom(int transactionId, IEnumerable<Item> items);
	}
}