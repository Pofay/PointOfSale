using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class NullObjectOrderRepository : OrderRepository
	{

		public void CreateOrder(int transactionId, IEnumerable<Item> orderItems)
		{

		}
	}
}

