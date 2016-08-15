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
		Dictionary<string, Item> itemPrices;


		public ItemRepository()
		{
			itemPrices = new Dictionary<string, Item>();
			itemPrices.Add("123456", new Item("123456", "Bowl", 12.50));
			itemPrices.Add("900000", new Item("900000", "Phone", 7.50));
			itemPrices.Add("456789", new Item("456789", "Crab", 24.50));
			itemPrices.Add("345670", new Item("345670", "Plunger", 6.50));
			itemPrices.Add("789010", new Item("789010", "Fish", 10.25));
		}

		public decimal getPriceFor(string barcode)
		{
			return itemPrices.ContainsKey(barcode) ? itemPrices[barcode].Price : new decimal(0.0);
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