using System.Linq;
using System.Collections.Generic;
using System;

namespace PointOfSale.Domain
{
	public class PointOfSaleService
	{
		private readonly OrderFulFiller orderFulFiller;
		private readonly ItemRegistryReader reader;
		private readonly List<Item> scannedItems;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IList<Item> ScannedItems { get { return scannedItems; } }

		public PointOfSaleService(ItemRegistryReader reader, OrderFulFiller orderFulFiller)
		{
			this.reader = reader;
			this.orderFulFiller = orderFulFiller;
			this.scannedItems = new List<Item>();
		}

		public event EventHandler<ScanEventArgs> OnScan;

		public void Scan(string barcode)
		{
			var item = reader.Read(barcode);
			OnScan?.Invoke(this, new ScanEventArgs(item));
			scannedItems.Add(item);
		}

		public void CompleteSale() // Should've received a Payment Parameter
		{
			// Display ought to display on Change
			// FulFillOrder(payment, ScannedItems) -> contains also the method for printing
			orderFulFiller.FulFillOrder(ScannedItems.ToList());
			ScannedItems.Clear();
		}

	}

}