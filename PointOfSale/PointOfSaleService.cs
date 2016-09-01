using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Domain
{
	public class PointOfSaleService
	{
		private readonly ItemService itemService;
		private readonly OrderFulFiller orderFulFiller;

		public decimal SubTotal { get { return itemService.SubTotal; } }
		public IList<Item> ScannedItems { get { return itemService.ScannedItems; } }

		public PointOfSaleService(ItemService itemService, OrderFulFiller orderFulFiller)
		{
			this.itemService = itemService;
			this.orderFulFiller = orderFulFiller;
		}

		public void Scan(string barcode)
		{
			itemService.AddItem(barcode);
		}

		public void OnCompleteSale()
		{
			orderFulFiller.FulFillOrder(ScannedItems.ToList());
			ScannedItems.Clear();
		}
	}

}