using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface OrderFulFiller
	{
		void FulFillOrder(IEnumerable<Item> orderItems);

	}
}