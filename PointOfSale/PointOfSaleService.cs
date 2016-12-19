using System.Linq;
using System.Collections.Generic;
using System;

namespace PointOfSale.Domain
{
	public class PointOfSaleService // The code screams that it should be a controller or a presenter
									// Not a service....
	{
		private readonly ScanBarcodeQuery query;
		private readonly List<Item> scannedItems;
		private readonly TransactionIdGenerator generator;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IList<Item> ScannedItems { get { return scannedItems; } }

		public PointOfSaleService(ScanBarcodeQuery query, TransactionIdGenerator generator)
		{
			this.query = query;
			this.generator = generator;
			this.scannedItems = new List<Item>();
		}

		public event EventHandler<ScannedBarcodeEventArgs> BarcodeEvent;
		public event EventHandler<CompleteSaleEventArgs> CompleteSaleEvent;

		public void OnBarcodeScan(string barcode)
		{
			var item = query.Read(barcode);
			BarcodeEvent?.Invoke(this, new ScannedBarcodeEventArgs(item));
			ScannedItems.Add(item);
		}

		public void OnCompleteSale() // Should've received a Payment Parameter
		{
			int id = generator.GenerateTransactionId();
			CompleteSaleEvent?.Invoke(this, new CompleteSaleEventArgs(id, ScannedItems.ToList()));
			ScannedItems.Clear();
		}
	}

}
