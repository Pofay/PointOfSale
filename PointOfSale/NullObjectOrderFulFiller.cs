using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class NullObjectOrderFulFiller : OrderFulFiller
	{

		public void FulFillOrder(IEnumerable<Item> orderItems)
		{

		}
	}
}

