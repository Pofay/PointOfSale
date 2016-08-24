using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class ItemService
	{
		private readonly ItemDisplay display;
		private readonly ItemRegistryReader reader;


		public ItemService(ItemRegistryReader reader, ItemDisplay display)
		{
			this.reader = reader;
			this.display = display;
		}

		public void AddItem(string barcode, IList<Item> items)
		{
			var item = reader.Read(barcode);
			items.Add(item);
			display.DisplayScannedItem(item);
		}
	}
}

