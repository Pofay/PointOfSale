using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Domain
{
	public class ItemRegistry
	{
		public ItemRegistry()
		{
		}

		public IEnumerable<Item> getAvailableItems()
		{
			var items = new List<Item>();
			items.Add(new Item("123456", "Bowl", 12.50));
			items.Add(new Item("900000", "Phone", 7.50));
			items.Add(new Item("456789", "Crab", 24.50));
			items.Add(new Item("345670", "Plunger", 6.50));
			items.Add(new Item("789010", "Fish", 10.25));
			return items;
		}
	}
}