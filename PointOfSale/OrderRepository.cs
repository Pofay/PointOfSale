using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface OrderRepository
	{
		void CreateOrder(int transactionId, IEnumerable<Item> orderItems);

	}
}