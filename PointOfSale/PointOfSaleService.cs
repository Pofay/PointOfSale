using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain
{

	public class PointOfSaleService
	{
		private readonly List<Item> scannedItems;
		private readonly ItemService itemService;
		private readonly ReceiptService receiptService;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IEnumerable<Item> ScannedItems { get { return scannedItems; } }

		// Display might be a decorator of some sort to prevent it to become a Header interface

		public PointOfSaleService(ItemService itemService, ReceiptService receiptService)
		{
			this.itemService = itemService;
			this.receiptService = receiptService;
			this.scannedItems = new List<Item>();
		}

		public void Scan(string barcode)
		{
			itemService.AddItem(barcode, scannedItems);
		}

		public void OnCompleteSale()
		{
			receiptService.CreateReceipt(SubTotal);
		}


	}

}