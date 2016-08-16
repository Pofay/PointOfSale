using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointOfSale.Domain
{

	public class Sale
	{
		private readonly List<Item> scannedItems;
		private decimal totalPrice;
		private readonly ItemRegistry repo;


		public decimal TotalPrice { get { return totalPrice; } }
		public IEnumerable<Item> ScannedItems { get { return scannedItems; } }

		public Sale(ItemRegistry repo)
		{
			this.repo = repo;
			this.scannedItems = new List<Item>();
		}


		public void OnBarcode(string barcode)
		{
			var item = repo.getItemWith(barcode);
			totalPrice = decimal.Add(totalPrice, item.Price);
			scannedItems.Add(item);
		}
	}

}