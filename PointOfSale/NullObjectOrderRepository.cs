using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class NullObjectOrderRepository : OrderFulFiller
	{

		public void FulFillOrder(int transactionId, IEnumerable<Item> orderItems)
		{

		}
	}
}

