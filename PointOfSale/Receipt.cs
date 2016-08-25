using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Domain
{
	public class Receipt
	{
		private readonly IEnumerable<Item> items;

		private readonly int id;

		public Receipt(int transactionId, IEnumerable<Item> items)
		{
			this.id = transactionId;
			this.items = items;
		}

		public override string ToString()
		{
			var receiptFormat = string.Format("Receipt For Transaction {0}\n", id); ;
			foreach (var item in items)
			{
				receiptFormat += string.Format("Item Name: {0} Price: {1}\n", item.Name, item.Price);
			}
			receiptFormat += string.Format("Sub Total: {0}", items.Sum(i => i.Price));
			return receiptFormat;
		}
	}
}