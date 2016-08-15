using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointOfSale.Domain
{

	public class ItemRepository
	{
		Dictionary<string, double> itemPrices;


		public ItemRepository()
		{
			itemPrices = new Dictionary<string, double>();
			itemPrices.Add("123456", 12.50);
			itemPrices.Add("900000", 7.50);
			itemPrices.Add("456789", 24.50);
		}

		public double getPriceFor(string barcode)
		{
			return itemPrices.ContainsKey(barcode) ? itemPrices[barcode] : 0.0;
		}

		public string getItemNameFor(string barcode)
		{
			if ("123456".Equals(barcode))
				return "Bowl";
			else if ("456789".Equals(barcode))
				return "Crab";
			else if ("789010".Equals(barcode))
				return "Fish";
			else if ("345670".Equals(barcode))
				return "Plunger";
			return "";
		}

	}

}