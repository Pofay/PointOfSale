using System;
namespace PointOfSale.Domain
{
	public class Item
	{
		private double price;

		public string Name { get; private set; }
		public decimal Price { get { return new decimal(price); } }
		public string Barcode { get; private set; }

		public Item(string barcode, string name, double price)
		{
			this.Barcode = barcode;
			this.Name = name;
			this.price = price;
		}
	}
}

