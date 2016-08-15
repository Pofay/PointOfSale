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
			if ("123456".Equals(barcode))
				itemNames.Add("Bowl");
			else if ("456789".Equals(barcode))
				itemNames.Add("Crab");
			else if ("789010".Equals(barcode))
				itemNames.Add("Fish");
			else if ("345670".Equals(barcode))
				itemNames.Add("Plunger");
		}
	}

}