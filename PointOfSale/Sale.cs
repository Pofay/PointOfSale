using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace PointOfSale.Domain
{

	public class Sale
	{
		private readonly List<Item> scannedItems;
		private decimal totalPrice;
		private readonly ItemRegistry repo;
		private readonly Display sut;

		public decimal TotalPrice { get { return totalPrice; } }
		public IEnumerable<Item> ScannedItems { get { return scannedItems; } }

		public Sale(ItemRegistry repo, Display sut)
		{
			this.repo = repo;
			this.sut = sut;
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