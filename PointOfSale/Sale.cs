using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain
{

	public class Sale
	{
		private readonly List<Item> scannedItems;
		private readonly ItemRegistryReader repo;
		private readonly Display display;
		private readonly ReceiptFactory factory;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IEnumerable<Item> ScannedItems { get { return scannedItems; } }

		// Display might be a decorator of some sort to prevent it to become a Header interface

		public Sale(ItemRegistryReader repo, Display display, ReceiptFactory factory)
		{
			this.repo = repo;
			this.display = display;
			this.factory = factory;
			this.scannedItems = new List<Item>();
		}

		public void Scan(string barcode)
		{
			var item = repo.Read(barcode);
			scannedItems.Add(item);
			display.DisplayScannedItem(item);
		}

		public void OnCompleteSale()
		{
			var receipt = factory.CreateReceiptFrom(SubTotal);
			display.DisplayReceipt(receipt);
		}
	}

}