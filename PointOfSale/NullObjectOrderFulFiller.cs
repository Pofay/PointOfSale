using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class NullObjectOrderFulFiller : CompleteSaleCommand
	{

		public void Execute(IEnumerable<Item> orderItems)
		{

		}
	}
}

