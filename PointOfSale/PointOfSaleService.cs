using System.Linq;
using System.Collections.Generic;
using System;

namespace PointOfSale.Domain
{
	public class PointOfSaleService
	{
		private readonly CompleteSaleCommand command;
		private readonly ScanBarcodeQuery query;
		private readonly List<Item> scannedItems;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IList<Item> ScannedItems { get { return scannedItems; } }

		public PointOfSaleService(ScanBarcodeQuery query, CompleteSaleCommand command)
		{
			this.query = query;
			this.command = command;
			this.scannedItems = new List<Item>();
		}

		public event EventHandler<ScannedBarcodeEventArgs> BarcodeEvent;

		public void OnBarcodeScan(string barcode)
		{
			var item = query.Read(barcode);
			BarcodeEvent?.Invoke(this, new ScannedBarcodeEventArgs(item));
			scannedItems.Add(item);
		}

		public void OnCompleteSale() // Should've received a Payment Parameter
		{
			// Display ought to display on Change
			// FulFillOrder(payment, ScannedItems) -> contains also the method for printing
			command.Execute(ScannedItems.ToList());
			ScannedItems.Clear();
		}
	}

}