using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Domain
{
	public interface ItemRegistry
	{
		IEnumerable<Item> getAvailableItems();
	}
}