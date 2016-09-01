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

		public decimal SubTotal { get { return scannedItems.Sum(i => i.Price); } }
		public IList<Item> ScannedItems { get { return scannedItems; } }

		public PointOfSaleService(ItemService itemService, OrderFulFiller orderFulFiller)
		{
			this.itemService = itemService;
			this.orderFulFiller = orderFulFiller;
			this.scannedItems = new List<Item>();
		}

		public void Scan(string barcode)
		{
			itemService.AddItem(barcode, scannedItems);
		}

		public void OnCompleteSale()
		{
			orderFulFiller.FulFillOrder(scannedItems.ToList());
			scannedItems.Clear();
		}
	}

}