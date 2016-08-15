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
		private readonly List<string> itemNames;
		private decimal totalPrice;
		private readonly ItemRegistry repo;


		public decimal TotalPrice { get { return totalPrice; } }
		public IEnumerable<string> ItemNames { get { return itemNames.ToArray(); } }

		public Sale(ItemRegistry repo)
		{
			this.repo = repo;
			this.itemNames = new List<string>();
		}


		public void OnBarcode(string barcode)
		{
			var item = repo.getItemWith(barcode);
			totalPrice = decimal.Add(totalPrice, item.Price);
			itemNames.Add(item.Name);
		}


	}

}