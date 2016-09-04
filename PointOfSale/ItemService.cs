using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Domain
{
	public class ItemService
	{
		private readonly ItemDisplay display;
		private readonly ItemRegistryReader reader;

		public IList<Item> ScannedItems { get; }
		public decimal SubTotal { get { return ScannedItems.Sum(i => i.Price); } }

		public ItemService(ItemRegistryReader reader, ItemDisplay display)
		{
			this.reader = reader;
			this.display = display;
			this.ScannedItems = new List<Item>();
		}

		public void AddItem(string barcode)
		{
			var item = reader.Read(barcode);
			ScannedItems.Add(item);
			//display.DisplayItem(item);
		}
	}
}

