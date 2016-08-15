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
		private double totalPrice;
		private readonly ItemRepository repo;


		public decimal TotalPrice { get { return new decimal(totalPrice); } }
		public IEnumerable<string> ItemNames { get { return itemNames.ToArray(); } }

		public Sale(ItemRepository repo)
		{
			this.repo = repo;
			this.itemNames = new List<string>();
		}


		public void OnBarcode(string barcode)
		{
			totalPrice += repo.getPriceFor(barcode);
			var itemName = repo.getItemNameFor(barcode);
			itemNames.Add(itemName);
		}


	}

}