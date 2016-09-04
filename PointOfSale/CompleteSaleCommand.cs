using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public interface CompleteSaleCommand
	{
		void Execute(IEnumerable<Item> orderItems);

	}
}