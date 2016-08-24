using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface ReceiptFactory
	{
		Receipt CreateReceiptFrom(IEnumerable<Item> items);
	}
}