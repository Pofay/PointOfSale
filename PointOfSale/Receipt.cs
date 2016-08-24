using System.Collections.Generic;

namespace PointOfSale.Domain
{
	public class Receipt
	{
		private readonly IEnumerable<Item> items;

		public Receipt(IEnumerable<Item> items)
		{
			this.items = items;
		}

		public override string ToString()
		{
			var receiptFormat = string.Empty;
			foreach (var item in items)
			{
				receiptFormat += string.Format("Item Name: {0} Price: {1}", item.Name, item.Price);
			}
			return receiptFormat;
		}
	}
}