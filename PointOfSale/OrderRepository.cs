using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface OrderRepository
	{
		void FulFillOrder(int transactionId, IEnumerable<Item> orderItems);

	}
}