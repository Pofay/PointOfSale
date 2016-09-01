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
		private readonly OrderFulFiller orderFulFiller;
		private readonly TransactionIdGenerator idGenerator;

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IList<Item> ScannedItems { get { return scannedItems; } }

		public PointOfSaleService(ItemService itemService, OrderFulFiller orderFulFiller, TransactionIdGenerator generator)
		{
			this.itemService = itemService;
			this.orderFulFiller = orderFulFiller;
			this.idGenerator = generator;
			this.scannedItems = new List<Item>();
		}

		public void Scan(string barcode)
		{
			itemService.AddItem(barcode, scannedItems);
		}

		public void OnCompleteSale()
		{
			int transactionId = idGenerator.GenerateTransactionId();
			orderFulFiller.FulFillOrder(transactionId, scannedItems.ToList());
			scannedItems.Clear();
		}
	}

}