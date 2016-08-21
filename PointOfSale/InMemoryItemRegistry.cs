using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain
{

	public class InMemoryItemRegistry : ItemRegistry, ItemRegistryReader
	{
		Dictionary<string, Item> items;


		public InMemoryItemRegistry()
		{
			items = new Dictionary<string, Item>();
			items.Add("123456", new Item("123456", "Bowl", 12.50));
			items.Add("900000", new Item("900000", "Phone", 7.50));
			items.Add("456789", new Item("456789", "Crab", 24.50));
			items.Add("345670", new Item("345670", "Plunger", 6.50));
			items.Add("789010", new Item("789010", "Fish", 10.25));
		}

		public Item getItemWith(string barcode)
		{
			return items.ContainsKey(barcode) ? items[barcode] : new Item("", "Unknown", 0.0);
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