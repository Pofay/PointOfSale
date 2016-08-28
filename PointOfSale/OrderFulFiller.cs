using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface OrderFulFiller
	{
		void FulFillOrder(int transactionId, IEnumerable<Item> orderItems);

	}
}