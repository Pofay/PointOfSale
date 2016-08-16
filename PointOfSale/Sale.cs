using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace PointOfSale.Domain
{

	public class Sale
	{
		private readonly List<Item> scannedItems;
		private decimal totalPrice;
		private readonly ItemRegistry repo;
		private readonly Display display;

		public decimal TotalPrice { get { return totalPrice; } }
		public IEnumerable<Item> ScannedItems { get { return scannedItems; } }

		// Display might be a decorator of some sort to prevent it to become a Header interface
		public Sale(ItemRegistry repo, Display display)
		{
			this.repo = repo;
			this.display = display;
			this.scannedItems = new List<Item>();
		}

		public void Scan(string barcode)
		{
			var item = repo.getItemWith(barcode);
			totalPrice = decimal.Add(totalPrice, item.Price);
			scannedItems.Add(item);
			display.DisplayScannedItem(item);
		}
	}

}