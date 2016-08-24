using System.Collections.Generic;
using System.Linq;

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
			var receiptFormat = "Receipt For Transaction\n";
			foreach (var item in items)
			{
				receiptFormat += string.Format("Item Name: {0} Price: {1}\n", item.Name, item.Price);
			}
			receiptFormat += string.Format("Sub Total: {0}", items.Sum(i => i.Price));
			return receiptFormat;
		}
	}
}